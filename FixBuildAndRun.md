# Fix Build & Run (Debug profile / exe missing)

If Visual Studio does not run the app properly, follow these steps:

1. **Clean and rebuild**
   - Build → Clean Solution
   - Build → Rebuild Solution

2. **Delete build folders**
   - Close Visual Studio
   - Delete `bin` and `obj` folders inside `KickBlastStudentUI`
   - Reopen solution and rebuild

3. **Set Startup Project**
   - In Solution Explorer, right-click `KickBlastStudentUI`
   - Choose **Set as Startup Project**

4. **Check configuration**
   - Ensure toolbar is set to **Debug** and **Any CPU**

5. **Verify launch profile**
   - `Properties/launchSettings.json` should use only:
     - `"commandName": "Project"`

Expected executable output path:
- `KickBlastStudentUI/bin/Debug/net8.0-windows/KickBlastStudentUI.exe`
