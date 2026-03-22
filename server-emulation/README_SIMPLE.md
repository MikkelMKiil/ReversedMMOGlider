# Simple Non-Encrypted Offset Server

## Overview

This is a simplified offset server for the MMOGlider client that serves WoW 3.3.5a memory offsets via plain HTTP/JSON - no encryption, no authentication. Perfect for development, testing, and understanding how the offset system works.

## Quick Start

### Start the Server

```bash
# Using default settings (port 8080, default offsets)
python3 simple_server.py

# Using custom port
python3 simple_server.py --port 9000

# Using custom offset file
python3 simple_server.py --offsets ../offsets/wotlk_3.3.5a_custom.json

# Bind to localhost only (more secure)
python3 simple_server.py --host 127.0.0.1 --port 8080
```

### Test the Server

```bash
# Get all offsets
curl http://localhost:8080/offsets

# Health check
curl http://localhost:8080/health

# Or open in browser
firefox http://localhost:8080/offsets
```

## API Endpoints

### GET /offsets
Returns all memory offsets in JSON format.

**Response:**
```json
{
  "status": "success",
  "version": "3.3.5a",
  "build": 12340,
  "timestamp": "2026-03-22T15:30:00.123456",
  "offsets": {
    "PlayerGUID": "0x00CD87A8",
    "TargetGUID": "0x00CD87B8",
    "ObjectManager_Base": "0x00CE87A0",
    ...
  }
}
```

### POST /offsets
Same as GET - for compatibility with clients expecting POST.

### GET /health
Health check endpoint.

**Response:**
```json
{
  "status": "healthy",
  "timestamp": "2026-03-22T15:30:00.123456",
  "offset_count": 50
}
```

## Offset File Format

Create a JSON file with your offsets:

```json
{
  "PlayerGUID": "0x00CD87A8",
  "TargetGUID": "0x00CD87B8",
  "ObjectManager": 13396128,
  "PlayerBase": 13396128,
  "Buff_Sickness": "15007"
}
```

Offsets can be:
- **Hex strings**: `"0x00CD87A8"` (recommended for readability)
- **Integers**: `13396128` (decimal)
- **String IDs**: `"15007"` (for buff IDs, etc.)

## Default Offsets

The server includes default WoW 3.3.5a (build 12340) offsets:

- Core pointers (PlayerGUID, TargetGUID, ObjectManager, etc.)
- Camera and world frame pointers
- Spell and casting function pointers
- Click-to-Move (CTM) structure offsets
- UI element offsets
- Sample buff IDs

See the `get_default_offsets()` function in `simple_server.py` for the complete list.

## Using with MMOGlider Client

To use this server with the MMOGlider client:

1. Start the simple server:
   ```bash
   python3 simple_server.py --port 8080
   ```

2. Modify the client's `ApplicationInitializer.cs` to point to this server (see next section)

3. Remove encryption from client offset retrieval

4. Run the client

## Modifying the Client

The client needs to be modified to:
1. Use HTTP GET instead of encrypted POST
2. Parse JSON response instead of binary protocol
3. Skip RSA/AES decryption

See `../docs/CLIENT_MODIFICATIONS.md` for detailed instructions.

## Advantages Over Encrypted Server

- **Simple**: No crypto libraries required
- **Debuggable**: Can inspect traffic with browser dev tools
- **Fast**: No encryption overhead
- **Transparent**: Easy to see what offsets are being served
- **Modifiable**: Easy to add/change offsets on the fly

## Security Note

This server has NO security features:
- No authentication
- No encryption
- No input validation
- Binds to 0.0.0.0 by default (accessible from network)

**Do not use this on public networks or production systems.**

For local development/testing only. Use `--host 127.0.0.1` to restrict to localhost.

## Extending the Server

### Adding New Offsets

Edit `simple_server.py` and modify the `get_default_offsets()` function, or create a JSON file:

```python
def get_default_offsets():
    return {
        "MyNewOffset": "0x12345678",
        "AnotherOffset": 0x87654321,
        # ...
    }
```

### Adding Custom Endpoints

Modify the `SimpleOffsetServer` class:

```python
def do_GET(self):
    if self.path == '/custom':
        self._send_json_response({'custom': 'data'})
```

### Logging Requests

All requests are logged to stderr with timestamps:
```
[2026-03-22 15:30:00] 127.0.0.1 - "GET /offsets HTTP/1.1" 200 -
```

## Comparison with Original Server

| Feature | Original Server | Simple Server |
|---------|----------------|---------------|
| Encryption | RSA + AES-256 | None |
| Protocol | Binary | JSON |
| Authentication | License check | None |
| Complexity | High | Low |
| Dependencies | pycryptodome | None (stdlib only) |
| Debuggability | Difficult | Easy |

## Files

- `simple_server.py` - Main server implementation
- `server.py` - Original encrypted server (for reference)
- `offsets/wotlk_3.3.5a.json` - Example offset database
- `README_SIMPLE.md` - This file

## Troubleshooting

### Port Already in Use
```
OSError: [Errno 98] Address already in use
```
**Solution**: Use a different port or kill the process using the port:
```bash
lsof -ti:8080 | xargs kill -9
python3 simple_server.py --port 9000
```

### Connection Refused from Client
- Check firewall settings
- Verify server is running: `curl http://localhost:8080/health`
- Check client is pointing to correct host/port

### Offsets Not Loading
- Verify JSON syntax in offset file
- Check file path is correct
- Look for error messages in server output

## License

Educational and research use only. Part of the MMOGlider historical preservation project.

## See Also

- `../offsets/wotlk_3.3.5a_offsets.md` - Complete WoW 3.3.5a offset documentation
- `../docs/ANALYSIS.md` - Analysis of original authentication system
- `server.py` - Original encrypted server implementation
