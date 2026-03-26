# Copilot Instructions

## Project Guidelines
- User prefers memory read/write logic to be centralized into one or two dedicated files, with clear naming and usage comments for maintainability/debugging.
- User requires crash/root-cause issues to be logged as critical errors, not generic logs.
- User prefers very low-token prompts when asking to craft requests for another AI.
- User wants target acquisition/resolution logic centralized in one location rather than spread across classes.

## Memory Management
- For WoW process memory pointers (LAA), read/validate addresses as unsigned/native pointers (e.g., uint/IntPtr), not signed int, to avoid negative overflow false-invalid checks.
- Replace usages of `long` with `ulong` because the 32-bit client should not use signed long values.
- When reading descriptor offsets, do not treat offset 0 as missing because OBJECT_FIELD_GUID at 0x00 is valid; use an explicit missing check instead of zero sentinel logic.

## Player GUID Management
- In this private-server setup, player GUID 0x1 is valid and can remain constant; do not treat GUID 0x1 as invalid by itself.