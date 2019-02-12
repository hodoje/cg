# cg
Faculty subject - Computer Graphics

**PZ1 assignment translation**

The goal of first subject assignment is to create an MVVM application for storing and showing photographs.

It's necessary to enable the user to register or login to application after it's launch. After logging in, the user will be shown all of its photographs that he saved to that time, with option for scrolling if it's more of them. After registration, the user needs to be able to save his first photograph. He needs to be able to add new photographs and change data about its account.

As a suggestion there can be one menu where user can choose one of the options - show his photographs, add new photograph and change account details.

Photographs are modeled with their path, headline and description. Beside that, timestamp of when the image was added also needs to be stored. User has his username and password. There should be validation of all input fields in the following way: path to the picture needs to exist, so as the headline. Description is optional. Username needs to be unique within the application and can be a combination of all characters, numbers and punctuations, but it cannot start with a number. Password needs to be longer than 6 characters.

It's necessary to implement photograph saving, and by that it means that after registration or login all photographs need to be loaded for chosen user. This can be solver either using simple database or XML file.

Storyboard looks like in the photograph.

**PZ2 assignment translation**

The goal of second subject assignment is to put elements of electroenergetics grid on a 2D map.

It's necessary to parse XML file with coordinates of elements (Geographic.xml) and then paint them on their geographic positions, including the power lines connecting those elements, using GMap API.

Documentation can be found on the following link: http://www.independent-software.com/gmap-net-tutorial-maps-markers-and-polygons.html
(These links get changed frequently so the best is to search for "gmap.net tutorial")

XML file contains elements assigned in UTM format. For more information on UTM format visit this link:
https://en.wikipedia.org/wiki/Universal_Transverse_Mercator_coordinate_system

In order to convert coordinates into decimal values so they could be used in C# code, a function, that will do the conversion, is given in UTMtoDecimalConversion.txt file.
For conversion check use this site: http://www.rcn.montana.edu/Resources/Converter.aspx.
Serbia is located in UTM zone 34.

GMap is created as a WindowsForms control, so in order to be used in WPF applications, the following is needed to do:
- First, in project references add the following references: **System.Windows.Forms**, **WindowsFormsIntegration**, and so the references for GMap DLLs (there are 2 of them). For GMap, in order to add them to the project, we need to reference them from the disk, so it's best to put them inside project folder.
- In XAML code of the window where the GMap will be used, it's necessary to special XML namespaces:
  - **xmlns:gmf="clr-namespace:GMap.NET.WindowsForms;assembly=GMap.NET.WindowsForms"**
  - **xmlns:gm="clr-namespace:GMap.NET;assembly=GMap.NET.Core"**
    - these are used for GMap API
  - **xmlns:wf="clr-namespace:System.Windows.Forms;assembly=System.Windows.Forms"**
    - this is used for usage of WindowsFormsHost tag

In the end, the window hierarchy should look like this:
  - Window stuff
     - `<Grid>`
        - `<WindowsFormsHost>`
           - `<gmf:GMapControl>`
