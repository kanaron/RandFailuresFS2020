# RandFailuresFS2020

If you like my work and want to support me you can donate me here:  
https://www.paypal.com/paypalme/kanaron  
Thank you :)

# About

Randomize in-flight failures and check if you can detect them, handle them and survive. From minor panel or turbocharger failure to complete brakes or engine failures.
You can try what would you do in case of
- Asymmetric flaps
- Gear failure
- Coolant leaks
- and more

In the field next to the name of the failure, you can enter the percentage chance of a failure in the range from 0‰ to 1000‰. Where 0‰ means that tere will be no failure, and 1000‰ means that failure will occur.
Failure will occur in one of four moments (which you can turn on or off) :
- instantly
- at random speed
- after random set of secconds
- at random altitide

![alt text](https://github.com/kanaron/RandFailuresFS2020/blob/master/Prev_1.jpg?raw=true)

This project won't be succesful without contributions from community

Thanks!

# Installation manual
1. Download latest release from https://github.com/kanaron/RandFailuresFS2020/releases
2. Unzip RandFailuresFS2020[version].zip archive to a directory

# Requirements
- Application requires .NET 6

# How to use
- Run application

- If you don't have flight simulator running yet, the state will be 'Sim not found', but after opening simulator it will change to 'Connected' and after flight start it will be 'Failures started'.

- By default you will have 'Main' preset available, but you can create your own by going to 'Preset' tab and clicking on 'New' button. Then you can modify the settings of your presets by clicking on 'Settings' button. It will redirect you to settings view where you can choose on which altitude, speed and time your failures could occur. You can also check the 'instant failure' checkbox if you'd like your failures to occur right after you start your flight. You can set the limit of failures that could occur.

- On 'Presets' tab you have 7 buttons that are corresponding to areas of failures occurrency e.g. gear, fuel, engine and so on. By clicking on it you will be redirected to the view where you can set the possibility in [‰] of your failure.

![alt text](https://github.com/kanaron/RandFailuresFS2020/blob/master/Prev_2.jpg?raw=true)

![alt text](https://github.com/kanaron/RandFailuresFS2020/blob/master/Prev_3.jpg?raw=true)

![alt text](https://github.com/kanaron/RandFailuresFS2020/blob/master/Prev_4.jpg?raw=true)

- When you already set settings for all the failures you wanted to, you can click on 'overview' and choose your newly prepared preset and start your flight on MS Flight Simulator 2020. You don't have to click on 'Start' it will start automatically right after you run your flight.

- While in the flight you can click "Restart" button and your failures will be randomized again. You can also click "Stop" button and change settings or preset and then click "Start" and your failures will be randomized based on new settings and preset. Keep in mind that if some failures already occured during your flight then they could stay even if you would click "Stop" or "Restart". Some failures can only be stopped by starting new flight.

- By clicking on 'Fail list' you will be redirected to view with list of failures generated for your flight - those will be revealed by clicking on 'Show failures' button. It's not recommended if you like surprises. Also if you restarted failures in current flight and some failures occured before restarting then list on "Fail list" tab could be incomplete. It's recommended to randomize your failures once per one flight.

Fly safe :)

# Author and contact
Mateusz "Kanaron" Godziek  
kanaron125@gmail.com  
discord: PL_Kanaron#5564
Languages: Polish, English 
