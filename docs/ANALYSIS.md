# MMOGlider Code Analysis - Preservation Documentation

## Executive Summary

This document provides a comprehensive analysis of the legacy MMOGlider bot software, focusing on game version compatibility, authentication mechanisms, and preservation strategies.

## Game Version Identification

### Target Game: World of Warcraft - Wrath of the Lich King (WotLK)

**Evidence from codebase:**

1. **README.md**: Explicitly states "early Wotlk" (Wrath of the Lich King)
2. **Release Date**: January 21, 2009 (StartupClass.cs:24)
3. **Software Version**: 1.8.0 Release (StartupClass.cs:25-26)
4. **Build ID**: 6703 (StartupClass.cs:27)
5. **.NET Framework**: v3.5 (Glider_fix-cleaned.csproj:10) - typical for 2009-era applications
6. **Class Support**: Includes Death Knight (DeathknightConfig.cs), which was introduced in WotLK expansion

### Likely WoW Client Version

Based on the release date (January 2009) and "early WotLk" designation, this targets:
- **WoW Client Version**: 3.0.x or 3.1.x series (early WotLK patches)
- **Possible builds**: 8606 (3.0.2), 9183 (3.0.8), or 9464 (3.0.9)
- **Expansion**: Wrath of the Lich King (2008-2010)

The software includes memory offset tables (MemoryOffsetTable.cs) that were downloaded from a remote server, suggesting it could adapt to different WoW client versions within the WotLK era.

## Authentication & Server-Side Architecture

### Authentication Mechanism

The software uses a **client-server license validation system** with the following components:

#### 1. Remote Authentication Server

- **Primary URL**: `http://www.mmoglider.com/EM.aspx`
- **Backup URL**: `http://vforums.mmoglider.com/GliderApp/EM.aspx`
- **Protocol**: HTTP POST with encrypted payload

#### 2. Encryption Details (GDataEncryptionManager.cs)

**Two-layer encryption scheme:**

a) **Symmetric Encryption (Rijndael/AES-256)**
   - Algorithm: RijndaelManaged
   - Key Size: 256-bit
   - Block Size: 256-bit
   - Mode: CBC (Cipher Block Chaining)
   - Padding: PKCS7
   - Random session key generated per connection

b) **Asymmetric Encryption (RSA)**
   - RSA public key embedded in client (lines 16-17)
   - Used to encrypt: session key + payload metadata
   - RSA modulus present in code (oR97bOVGOLZngLaX0hquQQXn76zCgVZCD4UhxNJJ1iZ1vpsdY4orqNni+dugxzFm5naMWb2ecqXt99lTD8CJfMePvrhhIo0qR8HiSSxKmkUIhuRBUv84LgB4rTE36xtIV76jkV7qbYsr8qmYh5iD7R/cswBFQwCqbnBalDK3L70=)

#### 3. Authentication Flow (ApplicationInitializer.cs)

```
Client                          Server (mmoglider.com)
  |                                    |
  |---(1) Generate AES session key---->|
  |---(2) Encrypt with RSA public----->|
  |---(3) Send: AppKey, Version------->|
  |                                    |
  |<--(4) Validate license-------------|
  |<--(5) Return encrypted:------------|
  |      - Memory offsets              |
  |      - Subscription status         |
  |      - Configuration data          |
  |      - Expiration date             |
```

#### 4. Data Exchanged During Authentication

**Sent to server:**
- Software version (1.8.0)
- Release type ("Release")
- WoW game version string
- Application key (from config)
- Various flags and parameters

**Received from server:**
- License validation status ("Good", "Beta", "Old")
- Subscription expiration date
- Memory offsets for current WoW version
- Configuration parameters
- Warning messages
- Realm restrictions (if any)

#### 5. Dynamic Memory Offsets

The software downloads memory offsets from the server (ApplicationInitializer.cs:103-134):
- Player object offsets
- UI element addresses
- Game state locations
- Buff/debuff tracking addresses

This allows the bot to adapt to different WoW client versions without code changes.

## Server Emulation Feasibility Assessment

### ✅ FEASIBLE - Server emulation is entirely possible

#### What Can Be Emulated:

1. **Authentication Server (EM.aspx endpoint)**
   - Accept encrypted client requests
   - Parse RSA-encrypted session key
   - Decrypt AES-encrypted payload
   - Return mock authentication success

2. **Memory Offset Distribution**
   - Provide static memory offsets for specific WoW client version
   - These offsets need to be reverse-engineered or documented for target WoW build

3. **License Validation**
   - Always return "Good" status
   - Set far-future expiration date
   - Skip realm restrictions

#### Technical Requirements:

1. **RSA Private Key**:
   - Need the matching private key for the embedded public key
   - OR replace public key in client binary and generate new keypair
   - OR patch client to skip RSA validation

2. **WoW Memory Offsets**:
   - Must be researched for target WoW version (3.0.x/3.1.x)
   - Can potentially extract from other sources or reverse engineer
   - Some offsets may already be documented in WoW bot preservation communities

3. **Server Implementation**:
   - Can be implemented in any language (C#, Python, Node.js, etc.)
   - Simple HTTP server with encryption handling
   - ~500-1000 lines of code

### Account Management (AutoLoginManager.cs)
The software includes an encrypted account storage system:
- Stores WoW account credentials locally
- Uses machine-specific encryption (EncryptedAccountStorage.cs)
- Automatically logs into WoW client
- Format: XML files in `Accounts/` directory

This component is **separate** from the license server and would work independently once the main authentication is bypassed.

### Web Notification Service (WebNotificationService.cs)

Optional feature that sends gameplay statistics to remote server:
- Player stats, location, experience gained
- Nearby players detection
- Chat logs
- Can be disabled or pointed to local endpoint

## Security & Anti-Detection Features

The software includes several anti-detection mechanisms:

1. **Warden Integration** (WardenMonitor.cs, WardenProtocol.cs)
   - Blizzard's anti-cheat system detection/evasion
   - Pattern matching against known Warden scans
   - Memory signature checks

2. **SecCheck Module** (SecCheck.cs)
   - Configuration safety checker
   - Warns about risky settings that increase detection risk

3. **Network Safety** (NetworkSafetyChecker.cs)
   - Validates network adapter configuration
   - MAC address handling
