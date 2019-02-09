# cg
Faculty subject - Computer Graphics

**PZ1 assignment translation**

The goal of first subject assignment is to create an MVVM application for storing and showing photographs.

It's necessary to enable the user to register or login to application after it's launch. After logging in, the user will be shown all of its photographs that he saved to that time, with option for scrolling if it's more of them. After registration, the user needs to be able to save his first photograph. He needs to be able to add new photographs and change data about its account.

As a suggestion there can be one menu where user can choose one of the options - show his photographs, add new photograph and change account details.

Photographs are modeled with their path, headline and description. Beside that, timestamp of when the image was added also needs to be stored. User has his username and password. There should be validation of all input fields in the following way: path to the picture needs to exist, so as the headline. Description is optional. Username needs to be unique within the application and can be a combination of all characters, numbers and punctuations, but it cannot start with a number. Password needs to be longer than 6 characters.

It's necessary to implement photograph saving, and by that it means that after registration or login all photographs need to be loaded for chosen user. This can be solver either using simple database or XML file.

Storyboard looks like in the photograph.
