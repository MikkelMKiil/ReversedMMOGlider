# MMOGlider Server Emulation

This directory contains a proof-of-concept emulation server for the MMOGlider authentication system.

## Purpose

The original MMOGlider client connects to `mmoglider.com` for license validation and to retrieve memory offsets. This server emulates that functionality to enable the historical software to function without the original infrastructure.

## Implementation Approaches

### Option A: Client Patching (Recommended for Quick Testing)

Modify `src/Core/ApplicationInitializer.cs` to bypass server validation:

```csharp
public static bool InitializeAndValidate(string inputString, bool isValidationRequired)
{
    // Bypass server validation
    StartupClass.IsSomeConditionMet = true;
    StartupClass.isInitializationSuccessful = true;

    // Set mock expiration far in future
    InitializationTime = DateTime.Now.AddYears(100);

    // Initialize memory offset table with hardcoded values
    MemoryOffsetTable.gclass18_0.method_0();
    // Add your WoW version offsets here:
    // MemoryOffsetTable.gclass18_0.method_2("PlayerBase", 0x12345678);
    // etc.

    return true;
}
```

### Option B: Python Server Emulation (Authentic)

See `server.py` for a complete implementation that:
- Accepts encrypted client requests
- Handles RSA + AES encryption
- Returns mock authentication success
- Serves memory offsets for specified WoW version

### Option C: Localhost Redirect (Easiest Setup)

1. Patch client to use `localhost:8080` instead of `mmoglider.com`
2. Run `server.py` locally
3. Client works without DNS/hosts file modifications

## Quick Start

### Prerequisites

```bash
pip install cryptography flask
```

### Running the Server

```bash
cd server-emulation
python server.py
```

Server will start on `http://localhost:8080`

### Configuring the Client

Edit `src/Authentication/GDataEncryptionManager.cs`:

```csharp
private const string MainUrl = "http://localhost:8080/EM.aspx";
private const string BackupUrl = "http://localhost:8080/EM.aspx";
```

Rebuild the client and it will connect to your local server.

## Memory Offsets

The server needs to provide memory offsets for your target WoW client version.

### Finding Offsets

Memory offsets can be obtained from:
1. WoW bot development communities (historical archives)
2. Reverse engineering the WoW client
3. Pattern scanning tools
4. Other preserved offset databases

### Offset Format

Offsets are stored as key-value pairs:

```python
offsets = {
    "PlayerBase": 0x00C79CE0,        # Base player object address
    "ObjectManager": 0x00C79C20,      # Object manager pointer
    "ClientConnection": 0x00C79D18,   # Network connection info
    "WorldMap": 0x00AE6F84,          # Current zone/map ID
    # ... hundreds more offsets
}
```

### WoW 3.0.9 (Build 9464) Example

See `offsets/wotlk_3.0.9.json` for a starter offset table (incomplete).

## Server Implementation

The server (`server.py`) implements:

1. **HTTP POST Handler** - Receives encrypted payloads
2. **RSA Decryption** - Decrypts session key
3. **AES Decryption** - Decrypts request payload
4. **Request Parsing** - Reads client version info
5. **Response Building** - Creates success response with offsets
6. **AES Encryption** - Encrypts response
7. **HTTP Response** - Returns to client

## Security Notes

- **Private Key Required**: The server needs the RSA private key matching the client's public key
- **Key Generation**: Or generate a new RSA keypair and patch the client
- **For Historical Use Only**: Not for production use

## Testing

Test the server without a full client:

```bash
cd server-emulation
python test_client.py
```

This sends a mock request and verifies the response.

## Advanced Configuration

### Custom Offset Sets

Create JSON files in `offsets/` directory:

```json
{
    "version": "3.0.9",
    "build": 9464,
    "offsets": {
        "PlayerBase": "0x00C79CE0",
        "ObjectManager": "0x00C79C20"
    }
}
```

Load with `--offsets offsets/wotlk_3.0.9.json`

### HTTPS Support

For authenticity, enable HTTPS:

```bash
python server.py --https --cert cert.pem --key key.pem
```

## Troubleshooting

### Client Can't Connect

- Verify server is running: `curl http://localhost:8080/EM.aspx`
- Check firewall settings
- Ensure client URL is updated

### Decryption Errors

- RSA key mismatch - regenerate keys and update client
- Wrong encryption parameters - verify Rijndael settings match

### Client Rejects Response

- Check offset format (must be hex integers)
- Verify all required fields are present
- Enable debug logging in client

## Files

- `server.py` - Main emulation server
- `test_client.py` - Test harness
- `offsets/` - Memory offset databases
- `keys/` - RSA key storage
- `README.md` - This file

## Future Enhancements

- [ ] Complete WoW 3.0.x offset database
- [ ] Support multiple WoW versions
- [ ] Web dashboard for offset management
- [ ] Automatic client patching tool
- [ ] Docker container for easy deployment

## References

- Original server: www.mmoglider.com (defunct)
- Encryption: RSA + AES-256 CBC
- See `../docs/ANALYSIS.md` for protocol details

---

*Server emulation for historical preservation purposes only*
