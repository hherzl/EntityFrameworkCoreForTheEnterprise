set startPath=%cd%
cd ..\..\db\
call createdb.bat
cd %startPath%
