@echo off
chcp 65001
setlocal

REM Base URL for raw files on GitHub (master branch)
set "BASE=https://raw.githubusercontent.com/xmusjackson/UnityEngine.BE.Il2CppAssetBundleManager/master"

REM Space-separated list of files to download
set "FILES=Il2CppAssetBundle.cs Il2CppAssetBundleManager.cs Il2CppAssetBundleRequest.cs InteropUtils.cs README.md LICENSE.txt NOTICE.md"

for %%F in (%FILES%) do (
    echo ── Downloading %%F
    curl -sSL "%BASE%/%%F" -o "%%F"
    if errorlevel 1 (
      echo    ⚠️  Failed to download %%F
    ) else (
      echo    ✅  Saved %%F
    )
)

echo.
echo All done!
pause
