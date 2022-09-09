# BasicSaveSystem
Unity package to use as a base for saving and loading data.

## Usage

### Game Data
Modify the GameData class : Add attributes for all the data you want to save/load.
For complex objects, you might have to manually break them down into multiples attributes; a good way to keep them "linked", you can define other classes.
Lists are a great way to keep the data of similar objects organised.

### Saving and Loading Manager
Add the DataPersistenceManager script to a GameObject. 
Call its StartLoading function to fill the GameData instance with previously saved information.
Call its SaveGame function to fill the GameData with new information; the GameData instance will then be serialized and a savefile is written. 

### IDataPersistence, how to save data of multiple objects
Any objects you want to save information about should implement the IDataPersistence interface.
This allows Unity to find all objects implementing that interface.
It also allows you to define how each object will be saved.
You must define how the data is written in the SaveData method and how it is used in the LoadData method. 

### FileDataHandler 
*You might have to change this file, but it will most likely be enough by default.*
This script handles the writting of the GameData to a file.
The save file is in the default directory for Unity persistent data.
The save file uses the JSON format but has a ".save" file extension.

### Encryption
If you choose to use encryption, you need to tick the "useEncryption" variable of DataPersistenceManager in the Unity Inspector.
For now, the encryption/decryption method is a simple binary OR.
Repeating the binary OR lets you get the message from before the encryption.
This method is by no means a good way to make the data safe, but it makes it harder for the user to directly change the savefile to cheat.
**You should change the passphrase used for the encryption.**
 

## Author
I am Camille BERNADAS, a french university student who aims to work as a Gameplay Programmer.
See https://camillebernadas.com for more stuff about me.

## License
MIT License
You are free to use, modify, redistribute this freely !
