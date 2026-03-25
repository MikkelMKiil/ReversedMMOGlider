# Copilot Instructions

## Project Guidelines
- User prefers memory read/write logic to be centralized into one or two dedicated files, with clear naming and usage comments for maintainability/debugging.
- User requires crash/root-cause issues to be logged as critical errors, not generic logs.

## Player GUID Management
- In this private-server setup, player GUID 0x1 is valid and can remain constant; do not treat GUID 0x1 as invalid by itself.