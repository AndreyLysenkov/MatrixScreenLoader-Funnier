You can see some commands below for programs.
To run program with parameters, you need press [Win] + [R] on key board and type:
	"%File path% %Parametr1%=%Value1% %Parametr2%=%Value2%"
		instead %File path% you need put full (with disk letter) path to program
		Example: "C:\Software\MatrixDigitFalls\MatrixScreenLoader.exe /height=27 /width=13 /run=1";
Key commands:
	/width [integer number] - set amount of digits on width of window;
	/height [integer number] - set amount of digits on height of window;
	/timeout [integer number] - set awaiting time in milliseconds between "frames" - "50" on default;
	/clear ["true" or "false"] - set is clear screen before adding new "frame" - "true" on default;
	/resize ["true" or "false"] - set is adjust to changes of window sizes - "true" on default;
	/range [integer number [2..9]] - set diapason of generated digits, 2 - for only 0 and 1 - "2" on default;
	/run [integer number [0..2]] - indicate that algorithm to run (0 - run on default) - "0" on default;
	/linesFile [file path] *'/run=2'* - indicate from where load lines, which would fall. Read each line from beginning of string - "none" on default; [Not done!];
	/minLinesLength [integer number] *'/run=2'* - set minimal length of lines - "1" on default;
	/maxLinesLength [integer number] *'/run=2'* - set maximal length of lines - "13" on default;
	/linesCount [integer number] *'/run=2'* - set maximal amount of lines - "2713" on default;
	/lineTimeout [integer number] *'/run=2'* - if positive indicate in that number of iteration add line; if negative, indicate how many lines generate per iteration - "-13" on default;
	/lineSpeed [integer number] *'/run=2'* - set maximal line speed - "13" on default;