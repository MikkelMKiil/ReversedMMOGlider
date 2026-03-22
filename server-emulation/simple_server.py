#!/usr/bin/env python3
"""
Simple Non-Encrypted MMOGlider Offset Server
--------------------------------------------
Provides WoW 3.3.5a memory offsets via simple HTTP JSON API.
No encryption, no authentication - purely for development/testing.

Usage:
    python simple_server.py [--port 8080] [--offsets offsets.json]

API Endpoints:
    GET  /offsets - Returns all offsets as JSON
    POST /offsets - Returns all offsets as JSON (for compatibility)
    GET  /health  - Health check endpoint

Example response:
    {
        "status": "success",
        "version": "3.3.5a",
        "build": 12340,
        "offsets": {
            "PlayerGUID": "0x00CD87A8",
            "TargetGUID": "0x00CD87B8",
            ...
        }
    }

Author: Historical Preservation Project
License: Educational/Research Use Only
"""

import sys
import json
import argparse
from http.server import HTTPServer, BaseHTTPRequestHandler
from datetime import datetime


class SimpleOffsetServer(BaseHTTPRequestHandler):
    """Simple HTTP handler for offset requests"""

    # Class variable to hold offset data
    offsets = {}

    def log_message(self, format, *args):
        """Override to add timestamp to logs"""
        timestamp = datetime.now().strftime('%Y-%m-%d %H:%M:%S')
        sys.stderr.write(f"[{timestamp}] {self.address_string()} - {format % args}\n")

    def _send_json_response(self, data, status=200):
        """Send JSON response"""
        json_data = json.dumps(data, indent=2)
        self.send_response(status)
        self.send_header('Content-Type', 'application/json')
        self.send_header('Content-Length', str(len(json_data)))
        self.send_header('Access-Control-Allow-Origin', '*')
        self.end_headers()
        self.wfile.write(json_data.encode('utf-8'))

    def do_GET(self):
        """Handle GET requests"""
        if self.path == '/offsets' or self.path == '/':
            self._send_json_response({
                'status': 'success',
                'version': '3.3.5a',
                'build': 12340,
                'timestamp': datetime.now().isoformat(),
                'offsets': self.offsets
            })
        elif self.path == '/health':
            self._send_json_response({
                'status': 'healthy',
                'timestamp': datetime.now().isoformat(),
                'offset_count': len(self.offsets)
            })
        else:
            self._send_json_response({
                'status': 'error',
                'message': 'Not found'
            }, status=404)

    def do_POST(self):
        """Handle POST requests - same as GET for compatibility"""
        if self.path == '/offsets' or self.path == '/':
            # Read request body (but ignore it for now)
            content_length = int(self.headers.get('Content-Length', 0))
            if content_length > 0:
                body = self.rfile.read(content_length)

            self._send_json_response({
                'status': 'success',
                'version': '3.3.5a',
                'build': 12340,
                'timestamp': datetime.now().isoformat(),
                'offsets': self.offsets
            })
        else:
            self._send_json_response({
                'status': 'error',
                'message': 'Not found'
            }, status=404)


def load_offsets_from_file(filepath):
    """Load offsets from JSON file"""
    try:
        with open(filepath, 'r') as f:
            return json.load(f)
    except FileNotFoundError:
        print(f"Error: Offset file '{filepath}' not found", file=sys.stderr)
        sys.exit(1)
    except json.JSONDecodeError as e:
        print(f"Error: Invalid JSON in '{filepath}': {e}", file=sys.stderr)
        sys.exit(1)


def get_default_offsets():
    """Return default WoW 3.3.5a offsets"""
    return {
        # Core pointers
        "PlayerGUID": "0x00CD87A8",
        "TargetGUID": "0x00CD87B8",
        "MouseoverGUID": "0x00CD87C8",
        "FocusGUID": "0x00CD87D8",
        "ObjectManager_Base": "0x00CE87A0",
        "ObjectManager_ActivePlayer": "0x00CE87B8",
        "ObjectManager_LocalGUID": "0x00CE87C8",

        # Integer versions (for compatibility with old code)
        "PlayerBase": 0x00CD87A8,
        "ObjectManager": 0x00CE87A0,
        "TargetBase": 0x00CD87B8,
        "MouseoverBase": 0x00CD87C8,

        # Camera
        "CameraPointer": "0x00D09850",
        "WorldFrame": "0x00CE87E0",

        # Spells
        "SpellCooldown_Base": "0x00CF3640",

        # Lua
        "LuaDoString": "0x00819210",
        "LuaGetLocalizedText": "0x007045F0",

        # Functions
        "ClntObjMgrGetActivePlayer": "0x00468550",
        "ClntObjMgrEnumVisibleObjects": "0x00468380",
        "ClntObjMgrObjectPtr": "0x00468360",

        # Casting
        "CastSpell": "0x0080BE50",
        "CastSpellByName": "0x0080C1F0",
        "StopCasting": "0x006E4CF0",
        "UseItem": "0x0075C8E0",

        # CTM (Click-to-Move)
        "CTM_Base": "0x00CA11B8",
        "CTM_X": "0x00CA11BC",
        "CTM_Y": "0x00CA11C0",
        "CTM_Z": "0x00CA11C4",
        "CTM_Action": "0x00CA11C8",
        "CTM_Distance": "0x00CA11CC",
        "CTM_GUID": "0x00CA11D0",

        # UI
        "ComboPoints": "0x00C5EC10",
        "RuneState": "0x00C5EC18",

        # Buff IDs (some examples)
        "Buff_Sickness": "15007",
        "Buff_WellFed": "25661",
    }


def main():
    """Main server entry point"""
    parser = argparse.ArgumentParser(
        description='Simple non-encrypted offset server for WoW 3.3.5a'
    )
    parser.add_argument(
        '--port',
        type=int,
        default=8080,
        help='Port to listen on (default: 8080)'
    )
    parser.add_argument(
        '--offsets',
        type=str,
        help='Path to JSON file containing offsets'
    )
    parser.add_argument(
        '--host',
        type=str,
        default='0.0.0.0',
        help='Host to bind to (default: 0.0.0.0)'
    )
    args = parser.parse_args()

    # Load offsets
    if args.offsets:
        offsets = load_offsets_from_file(args.offsets)
        print(f"Loaded {len(offsets)} offsets from {args.offsets}")
    else:
        offsets = get_default_offsets()
        print(f"Using {len(offsets)} default WoW 3.3.5a offsets")

    # Set offsets in handler class
    SimpleOffsetServer.offsets = offsets

    # Start server
    server_address = (args.host, args.port)
    httpd = HTTPServer(server_address, SimpleOffsetServer)

    print(f"\n{'=' * 60}")
    print(f"Simple Offset Server Running")
    print(f"{'=' * 60}")
    print(f"Listening on: http://{args.host}:{args.port}")
    print(f"Endpoints:")
    print(f"  - GET  /offsets - Get all offsets")
    print(f"  - POST /offsets - Get all offsets (compatibility)")
    print(f"  - GET  /health  - Health check")
    print(f"\nPress Ctrl+C to stop")
    print(f"{'=' * 60}\n")

    try:
        httpd.serve_forever()
    except KeyboardInterrupt:
        print("\nShutting down server...")
        httpd.shutdown()


if __name__ == '__main__':
    main()
