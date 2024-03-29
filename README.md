# checkout-cafe
### This project is as point of sale system for a cafe, made with WPF and C#

## Development Environment Standard
### IDE
Visual Studio 2022, latest community version at the time of commit download
[here](https://visualstudio.microsoft.com/downloads/)

### OS
* Windows 10 Education, version 21H2
* OS-version: 19044.3693

### Programming Languages
* C# v. 12
* .NET v. 8.0
  
### Coding Standard
* Follow the .editorconfig file for coding standard, located in the project folder named "checkout-system-cafe"

### Extensions
* FlaUI.UIA3

## Documentation
### Setup 
* Install FlaUI according to the instructions in the following [video](https://www.youtube.com/watch?v=86wfAnfgqGg&list=PLacgMXFs7kl_fuSSe6lp6YRaeAp6vqra9&index=7&ab_channel=HYRTutorials)
* Install FlaUInspect to find names, IDs and file paths easier
  * Download [here](https://github.com/FlaUI/FlaUInspect/releases/)
  * Instructions for use [here](https://www.youtube.com/watch?v=790e_YlV16A&list=PLacgMXFs7kl_fuSSe6lp6YRaeAp6vqra9&index=9&ab_channel=HYRTutorials)
* To run the tests, you have to debug the checkout-system-cafe program
  * It should look like this, if it does then press the green run button <br>
  ![image](https://github.com/NTIG-Uppsala/checkout-system-cafe/assets/142985254/7dcb007e-2bc1-461c-acf5-a50dc0560df1)
  * The following files should then be created in the net8.0-windows folder: <br>
  ![image](https://github.com/NTIG-Uppsala/checkout-system-cafe/assets/142985254/3515fd99-8014-4a83-914c-21d9e554753d)
* If any code is changed in the checkout-system-cafe project then you have to debug again following the instructions above
  * This is for tests to function correctly  
### Use the .editorconfig file (code analysis system)
* To use the code analysis system, you must start it manually
* Run the command "dotnet format" in the terminal
* The system will then review and correct the code, so it follows the editorconfig standard

### Publish released program
* Open the project in Visual Studio, which you want to publish
* Enter command "dotnet publish" in the Visual Studio terminal
* Path to the created release is mentioned in the terminal
* Select all files in the publish folder, zip all and rename the zipped file "CheckoutSystemCafe.zip"
* Follow these [instructions](https://docs.github.com/en/repositories/releasing-projects-on-github/managing-releases-in-a-repository#creating-a-release) in order to publish the release on GitHub

### Run released program
* Enter the following [link](https://github.com/NTIG-Uppsala/checkout-system-cafe/releases/latest)
* Download "CheckoutSystemCafe.zip" underneath "Assets"
* Extract the downloaded folder
* Enter the "CheckoutSystemCafe" folder
* Run the "checkout-system-cafe.exe" file, the program window should then start
* If you get "Windows has protected your computer" then press "More information"
* ![image](https://github.com/NTIG-Uppsala/checkout-system-cafe/assets/142982637/0f54506a-fe7e-45a2-938e-7afa9d922ff1)
* And then you press "run anyway", the program window should then start
* ![image](https://github.com/NTIG-Uppsala/checkout-system-cafe/assets/142982637/6b770417-15a3-46bf-bc1b-018a39bd2cc4)
