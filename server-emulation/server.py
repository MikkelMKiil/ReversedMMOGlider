#!/usr/bin/env python3
"""
MMOGlider Authentication Server Emulator
-----------------------------------------
Emulates the mmoglider.com authentication server for historical preservation.

This server:
1. Accepts encrypted client requests (RSA + AES-256)
2. Validates license (always returns success)
3. Returns memory offsets for WoW client
4. Enables historical software to function

Usage:
    python server.py [--port 8080] [--offsets offsets.json]

Author: Historical Preservation Project
License: Educational/Research Use Only
"""

import sys
import json
import struct
from datetime import datetime, timedelta
from http.server import HTTPServer, BaseHTTPRequestHandler
from io import BytesIO

try:
    from Crypto.Cipher import AES, PKCS1_v1_5
    from Crypto.PublicKey import RSA
    from Crypto.Random import get_random_bytes
except ImportError:
    print("ERROR: pycryptodome required. Install with: pip install pycryptodome")
    sys.exit(1)


class AuthenticationServer:
    """Handles MMOGlider authentication protocol"""

    def __init__(self, offset_file=None):
        """Initialize server with optional offset database"""
        self.offsets = self._load_offsets(offset_file)
        # Generate or load RSA keypair
        # Note: In production, you'd use the original private key
        # or patch the client with your public key
        self.rsa_key = RSA.generate(1024)

    def _load_offsets(self, offset_file):
        """Load memory offsets from JSON file"""
        if offset_file:
            try:
                with open(offset_file, 'r') as f:
                    return json.load(f)
            except FileNotFoundError:
                print(f"Warning: Offset file {offset_file} not found")

        # Default mock offsets (incomplete - for demonstration)
        return {
            "PlayerBase": 0x00C79CE0,
            "ObjectManager": 0x00C79C20,
            "ClientConnection": 0x00C79D18,
            "WorldMap": 0x00AE6F84,
            "TargetGUID": 0x00BD07B0,
            # Add more offsets as discovered...
        }

    def handle_request(self, encrypted_data):
        """Process encrypted client request and return response"""
        try:
            # Parse encrypted request structure
            # Format: [4 bytes: RSA length][RSA encrypted header][AES encrypted payload]

            # Read RSA encrypted header length
            rsa_length = struct.unpack('<I', encrypted_data[:4])[0]

            # Extract RSA encrypted portion (contains AES key)
            rsa_encrypted = encrypted_data[4:4 + rsa_length]

            # Extract AES encrypted payload
            aes_encrypted = encrypted_data[4 + rsa_length:]

            # Decrypt RSA portion to get AES session key
            cipher_rsa = PKCS1_v1_5.new(self.rsa_key)
            decrypted_header = cipher_rsa.decrypt(rsa_encrypted, None)

            if not decrypted_header:
                return self._error_response("RSA decryption failed")

            # Parse decrypted header: [4 bytes: version][4 bytes: payload length][32 bytes: AES key]
            aes_key = decrypted_header[8:40]

            # Initialize AES cipher (Rijndael 256-bit CBC)
            # IV is predictable (64, 63, 62, ... 33)
            iv = bytes(range(64, 32, -1))
            cipher_aes = AES.new(aes_key, AES.MODE_CBC, iv)

            # Decrypt payload
            decrypted_payload = cipher_aes.decrypt(aes_encrypted)

            # Parse request (simplified - actual format is more complex)
            # Format: [int: packet_type][int: customer_id][string: app_key][int: version]...
            request = self._parse_request(decrypted_payload)

            print(f"[AUTH] Client request: version={request.get('version', 'unknown')}")

            # Build success response
            response = self._build_response(request)

            # Encrypt response with same AES key
            cipher_aes_response = AES.new(aes_key, AES.MODE_CBC, iv)
            encrypted_response = cipher_aes_response.encrypt(self._pad(response))

            return encrypted_response

        except Exception as e:
            print(f"[ERROR] Exception handling request: {e}")
            import traceback
            traceback.print_exc()
            return self._error_response(f"Server error: {e}")

    def _parse_request(self, data):
        """Parse decrypted client request"""
        # Simplified parsing - actual format is more complex
        # See GDataEncryptionManager.cs PrepareDataStream()
        request = {}
        stream = BytesIO(data)

        try:
            # Read packet type
            request['packet_type'] = struct.unpack('<I', stream.read(4))[0]

            # Read customer/user ID
            request['customer_id'] = struct.unpack('<I', stream.read(4))[0]

            # Read app key (string)
            key_length = struct.unpack('<I', stream.read(4))[0]
            if key_length > 0 and key_length < 1000:
                request['app_key'] = stream.read(key_length).decode('ascii', errors='ignore')

            # Read version info
            request['client_version'] = struct.unpack('<I', stream.read(4))[0]
            request['game_version'] = struct.unpack('<I', stream.read(4))[0]

        except Exception as e:
            print(f"[WARN] Parse error: {e}")

        return request

    def _build_response(self, request):
        """Build authentication response"""
        response = BytesIO()

        # Write response status
        self._write_string(response, "Good")  # Status: Good/Bad/Beta/Old

        # Write response message
        self._write_string(response, "Welcome! License validated.")

        # Write update URL (empty = no update needed)
        self._write_string(response, "")

        # Write subscription info
        response.write(struct.pack('<I', 1))  # Days remaining (not used if expiry set)

        # Write warning message (empty)
        self._write_string(response, "")

        # Write warning URL (empty)
        self._write_string(response, "")

        # Write realm restriction (empty = no restriction)
        self._write_string(response, "")

        # Write account type
        self._write_string(response, "Release")  # or "Beta"

        # Write max sessions
        response.write(struct.pack('<I', 999))

        # Write some usage counter
        response.write(struct.pack('<I', 1))

        # Write subscription expiration date
        expiry = datetime.now() + timedelta(days=36500)  # 100 years
        self._write_string(response, expiry.strftime("%m/%d/%Y %H:%M:%S"))

        # Write time to add (seconds)
        response.write(struct.pack('<I', 0))

        # Write memory offsets
        response.write(struct.pack('<I', len(self.offsets)))

        for key, value in self.offsets.items():
            self._write_string(response, key)
            self._write_string(response, f"{value:X}")  # Hex format

        return response.getvalue()

    def _write_string(self, stream, s):
        """Write length-prefixed string"""
        encoded = s.encode('ascii')
        stream.write(struct.pack('<I', len(encoded)))
        stream.write(encoded)

    def _pad(self, data):
        """PKCS7 padding for AES"""
        pad_length = 32 - (len(data) % 32)
        return data + bytes([pad_length] * pad_length)

    def _error_response(self, message):
        """Return error response"""
        response = BytesIO()
        self._write_string(response, "Bad")
        self._write_string(response, message)
        self._write_string(response, "")
        return self._pad(response.getvalue())


class RequestHandler(BaseHTTPRequestHandler):
    """HTTP request handler for authentication endpoint"""

    def do_POST(self):
        """Handle POST request to /EM.aspx"""
        if self.path not in ['/EM.aspx', '/GliderApp/EM.aspx']:
            self.send_error(404)
            return

        try:
            # Read request body
            content_length = int(self.headers['Content-Length'])
            request_data = self.rfile.read(content_length)

            print(f"[HTTP] Received {content_length} bytes from {self.client_address[0]}")

            # Process authentication
            response_data = self.server.auth_server.handle_request(request_data)

            # Send response
            self.send_response(200)
            self.send_header('Content-Type', 'application/octet-stream')
            self.send_header('Content-Length', str(len(response_data)))
            self.end_headers()
            self.wfile.write(response_data)

            print(f"[HTTP] Sent {len(response_data)} bytes response")

        except Exception as e:
            print(f"[ERROR] Request handling failed: {e}")
            self.send_error(500)

    def log_message(self, format, *args):
        """Suppress default logging"""
        pass


def main():
    """Start authentication server"""
    import argparse

    parser = argparse.ArgumentParser(description='MMOGlider Authentication Server Emulator')
    parser.add_argument('--port', type=int, default=8080, help='Port to listen on')
    parser.add_argument('--offsets', help='Path to offset JSON file')
    parser.add_argument('--host', default='localhost', help='Host to bind to')

    args = parser.parse_args()

    # Create authentication server
    auth_server = AuthenticationServer(offset_file=args.offsets)

    # Create HTTP server
    server = HTTPServer((args.host, args.port), RequestHandler)
    server.auth_server = auth_server

    print(f"""
╔═══════════════════════════════════════════════════════════╗
║   MMOGlider Authentication Server Emulator               ║
║   Historical Preservation Project                        ║
╚═══════════════════════════════════════════════════════════╝

Server running at: http://{args.host}:{args.port}
Endpoint: /EM.aspx

Loaded {len(auth_server.offsets)} memory offsets
Press Ctrl+C to stop

[INFO] Waiting for client connections...
    """)

    try:
        server.serve_forever()
    except KeyboardInterrupt:
        print("\n\n[INFO] Server shutting down...")
        server.shutdown()


if __name__ == '__main__':
    main()
