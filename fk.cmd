@ECHO OFF

where fake.exe 1>NUL 2>NUL

IF ERRORLEVEL 1 (
   ECHO ERROR: FAKE not found. Install it by running
   ECHO dotnet tool install fake-cli --global
   EXIT /b %errorlevel%
)

SET FAKE_DETAILED_ERRORS=true
fake.exe %*