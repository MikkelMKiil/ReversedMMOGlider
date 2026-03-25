# Copilot Instructions

## Project Guidelines
- User prefers memory read/write logic to be centralized into one or two dedicated files, with clear naming and usage comments for maintainability/debugging.
- User requires crash/root-cause issues to be logged as critical errors, not generic logs.
- User prefers very low-token prompts when asking to craft requests for another AI.

## Memory Management
- For WoW process memory pointers (LAA), read/validate addresses as unsigned/native pointers (e.g., uint/IntPtr), not signed int, to avoid negative overflow false-invalid checks.
- Replace usages of `long` with `ulong` because the 32-bit client should not use signed long values.

## Player GUID Management
- In this private-server setup, player GUID 0x1 is valid and can remain constant; do not treat GUID 0x1 as invalid by itself.