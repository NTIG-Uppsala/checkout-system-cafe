# checkout-cafe

## Development enviroment standard
### IDE
Visual Studio 2022, latest community version at the time of commit
https://learn.microsoft.com/en-us/visualstudio/get-started/csharp/tutorial-wpf?view=vs-2022

### OS
* Windows 10 
* Edition: Windows 10 Education
* Version: 21H2
* OS-version: 19044.3693

## Code standard
### Programming Languanguages
* C# v. 12
* .NET v. 8.0
  
### Standard
* Follow the .editorconfig folder for coding standard, located in the folder

### Extensions
* FlaUI.UIA3

## Documentation
### Setup 
* Install FlaUI according to the instructions in the following [video](https://www.youtube.com/watch?v=86wfAnfgqGg&list=PLacgMXFs7kl_fuSSe6lp6YRaeAp6vqra9&index=7&ab_channel=HYRTutorials)
* Install FlaUI inspect to find names, IDs and file paths easier.
  * Download [here](https://github.com/FlaUI/FlaUInspect/releases/)
  * Instructions for use [here](https://www.youtube.com/watch?v=790e_YlV16A&list=PLacgMXFs7kl_fuSSe6lp6YRaeAp6vqra9&index=9&ab_channel=HYRTutorials)
* To run the tests you have to debug the checkout-system-cafe program
  * It should look like this, if it does then press the green run button <br>
  ![image](https://github.com/NTIG-Uppsala/checkout-system-cafe/assets/142985254/7dcb007e-2bc1-461c-acf5-a50dc0560df1)
  * The following files should then be created in the net8.0-windows folder: <br>
  ![image](https://github.com/NTIG-Uppsala/checkout-system-cafe/assets/142985254/3515fd99-8014-4a83-914c-21d9e554753d)
* If any code is changed in the checkout-system-cafe project then you have to debug again following the instructions above
  * This is for tests to function correctly  
### Use the .editorconfig file (code analasysis system)
* To use the code analysis system you have to start it manually.
* Run the command "dotnet format" in the terminal
* The system will then review and correct the code so it follows the editorconfig standard


