@echo off
mkdir temp
copy ..\output\debug\Solid.exe temp
copy ..\output\debug\Palaso*.dll temp
copy ..\output\debug\Solid*.dll temp
copy ..\output\debug\Key*.dll temp
mkdir temp\exporters
copy ..\exporters\*.* temp\exporters

mkdir temp\mappings
copy ..\mappings\*.* temp\mappings

mkdir temp\templates
copy ..\templates\*.* temp\templates

cd temp
bsdtar -czf ..\output\solid.tgz .
cd ..
rmdir /S /Q temp
