# MMOGlider - Historical Software Preservation

This repository contains preserved source code for MMOGlider, a historic World of Warcraft bot from the early Wrath of the Lich King era (circa January 2009). This is an archaeological preservation effort to maintain gaming history and understand the evolution of game automation and anti-cheat systems.

## ⚠️ Legal & Ethical Notice

This software was designed to violate Blizzard Entertainment's Terms of Service. This preservation is for **historical, educational, and research purposes only**.

- **Do NOT use on live game servers**
- Subject of legal precedent: *MDY Industries, LLC v. Blizzard Entertainment, Inc.* (2008-2011)
- Useful for understanding anti-cheat evolution and game security history

## Project Information

- **Target Game**: World of Warcraft: Wrath of the Lich King
- **WoW Version**: 3.0.x - 3.1.x (early WotLK, 2008-2009)
- **Software Version**: 1.8.0 Release (Build 6703)
- **Release Date**: January 21, 2009
- **Platform**: Windows (XP/Vista/7)
- **Technology**: C# .NET Framework 3.5

## Repository Structure

```
/src
  /Core                    - Core engine and initialization
  /Memory                  - Memory manipulation and offset management
  /Authentication          - Server communication and encryption
  /UI                      - Windows Forms GUI components
    /Forms                 - Main application windows
    /Components            - UI widgets and controls
  /Game                    - WoW-specific game logic
    /Combat                - Combat controllers and spell management
    /Classes               - Class-specific AI (all 10 WotLK classes)
    /Movement              - Pathfinding and navigation
    /Objects               - Game object representations
    /GliderCommon          - Shared game utilities and default scripts
  /Automation              - Bot logic and profile system
  /Security                - Warden anti-cheat detection/evasion
  /Utils                   - Utilities and helpers

/docs                      - Preservation documentation
  /ANALYSIS.md             - Comprehensive technical analysis
  /README.md               - Original project readme

/server-emulation          - Server emulation components
```

## Documentation

See [**docs/ANALYSIS.md**](docs/ANALYSIS.md) for comprehensive technical documentation including:

- Game version identification methodology
- Authentication server architecture analysis
- Encryption scheme details (RSA + AES-256)
- Memory offset system explanation
- Server emulation feasibility assessment
- Anti-detection mechanisms (Warden evasion)
- Historical context

## Key Technical Features

### Authentication System
- Client-server license validation
- Two-layer encryption (RSA + AES-256 Rijndael)
- Dynamic memory offset distribution
- Original servers: mmoglider.com (defunct)

### Bot Capabilities
- All 10 WotLK class support (including Death Knight)
- Combat rotation automation
- Pathfinding and navigation
- Quest profile system
- Party/raid support
- Anti-detection features
- Warden anti-cheat evasion

### Architecture Highlights
- Memory manipulation using Windows APIs
- Pattern matching for game structure detection
- Encrypted account storage
- Remote viewing capabilities
- Extensive configuration system

## Server Emulation

**Status**: ✅ Feasible

The authentication server can be emulated using several approaches:
1. Client patching (easiest - bypass validation)
2. Local server implementation (authentic)
3. Hybrid approach (recommended)

See [docs/ANALYSIS.md](docs/ANALYSIS.md) for detailed implementation guidance.

## Building

**Requirements:**
- Visual Studio 2008/2010 or later
- .NET Framework 3.5 SDK
- Windows platform

**Build:**
```bash
# Open in Visual Studio
start Glider_fix-cleaned.sln

# Or build via MSBuild
msbuild Glider_fix-cleaned.sln /p:Configuration=Release
```

**Note**: Building requires memory offsets for your target WoW version. See authentication bypass instructions in ANALYSIS.md.

## Historical Context

MMOGlider was one of the most sophisticated World of Warcraft bots of its era. It featured:
- Advanced AI for all character classes
- Sophisticated anti-detection mechanisms
- Regular updates for new WoW patches
- Commercial distribution model

The software led to significant legal precedent regarding:
- Software copyright and EULA enforcement
- Anti-circumvention provisions (DMCA 1201)
- Virtual property rights
- Terms of Service enforceability

## Preservation Goals

1. ✅ **Document game version and architecture**
2. ✅ **Restructure code for readability**
3. ✅ **Analyze authentication mechanisms**
4. 🔲 **Create server emulation implementation**
5. 🔲 **Extract and document memory offsets**
6. 🔲 **Educational materials on anti-cheat history**

## Research Applications

This codebase is valuable for:
- Game security research and education
- Anti-cheat system evolution studies
- Historical software preservation
- Legal/academic research on virtual worlds
- Understanding 2000s-era game automation techniques

## Contributing

Contributions welcome for:
- Documentation improvements
- Historical context and timeline
- Server emulation implementation
- Memory offset documentation
- Build/compatibility fixes

## License

Original software authorship unknown (decompiled from binary). This preservation effort is for educational purposes under fair use doctrine for historical preservation and research.

## References

- **Legal Case**: MDY Industries, LLC v. Blizzard Entertainment, Inc., 629 F.3d 928 (9th Cir. 2010)
- **Original Site**: www.mmoglider.com (defunct)
- **WoW Version History**: Wrath of the Lich King (2008-2010)

## Acknowledgments

This preservation effort aims to maintain an important piece of gaming history while respecting intellectual property rights and promoting understanding of game security evolution.

---

*Preserved March 2026 for historical and educational purposes*
