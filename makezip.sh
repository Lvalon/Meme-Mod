#!/bin/sh
cd /Users/e/Library/Application\ Support/Steam/steamapps/common/LBoL/BepInEx/plugins
mkdir lvalonmeme
rm -r lvalonmeme/
cp -R -a /Users/e/Desktop/tachyon\ transmigration/projects/indev/lvalonmeme/DIRRESOURCES/. lvalonmeme/
cp -a /Users/e/Desktop/tachyon\ transmigration/projects/indev/lvalonmeme/bin/Debug/netstandard2.1/lvalonmeme.dll lvalonmeme/
cp -a /Users/e/Desktop/tachyon\ transmigration/projects/indev/lvalonmeme/CHANGELOG.md lvalonmeme/
cp -a /Users/e/Desktop/tachyon\ transmigration/projects/indev/lvalonmeme/icon.png lvalonmeme/
cp -a /Users/e/Desktop/tachyon\ transmigration/projects/indev/lvalonmeme/manifest.json lvalonmeme/
cp -a /Users/e/Desktop/tachyon\ transmigration/projects/indev/lvalonmeme/README.md lvalonmeme/
cp -a /Users/e/Desktop/tachyon\ transmigration/projects/indev/lvalonmeme/modinfo.json lvalonmeme/
rm -fr lvalonmeme/Thumbs.db
zip -r -j lvalonmeme.zip lvalonmeme/*
