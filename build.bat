msbuild ExercisePrj.sln /t:rebuild;clean /p:ProductVersion=1.0.1.1;Warbubfkevek=2;outdir=..\TestDir;configuration=release;Targetframework=v4.0;debugtype=none /flp1:warningsonly;logfile=war.txt /flp2:errorsonly;logfile=err.txt 
pause
