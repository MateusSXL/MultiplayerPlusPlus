#!/bin/sh

modVersion=0
#gameVersion="1.2"
modName="multiplayer-plus-plus"
projectName="MultiplayerPlusPlusMod"
installationPath="../../../SteamLibrary/steamapps/common/Skater XL/Mods/"
buildMode="Release"

msbuild.exe -v:q ./$projectName/$projectName.sln /property:Configuration=$buildMode
if [[ $? = 0 ]]
then
    echo "[msbuild] SUCCESS"
    modVersion=$(../jq-win64.exe -r .Version ./$modName/info.json)
    cp ./$projectName/$projectName/bin/$buildMode/$projectName.dll ./$modName/$projectName.dll
    7z.exe a -tzip $modName-$modVersion.zip ./$modName
    if [[ $? = 0 ]]
    then
        echo "[7z] Mod compressed into $modName-$modVersion.zip"
    else
        echo "[7z] ERROR"
    fi
    cp -r ./$modName "$installationPath"
    echo "MOD INSTALLED."
else
    echo "[msbuild] ERROR"
fi