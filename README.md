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

The goal of second subject assignment is to put elements of power grid on a 2D map.

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
           
**Third assignment translation**

The goal of third subject assignment is to draw a graph of a power network.

It's necessary to draw a network graph based on Geographic.xml file.

Network graph approximates the network on an orthogonal view in the form of a grid. First, it's needed to define a grid, the more rows and columns, the more the view will be in more detail. After that, coordinates are loaded from xml file and the nodes are drawn, in a way that they're approximated to the nearest point on the grid (point is an intersection between horizontal and vertical lines).

Each node is approximated on a grid. Nodes are drawn on the grid in a way that an image (graphical element) is drawn that will represent the type of node (Substation, Node, Switch). For each node there is a ToolTip shown with information about what element is drawn there.

**1-a:** Nodes are approximated to nearest grid point and in that case, the nodes can overlap each other. If overlap happens, on that place draw some special image that represents a group, and in ToolTip show information about each element in that group. **(2 points)**

**1-b:** Nodes are approximated to the nearest free grid point. In this case, it's necessary to take care of minimal grid dimension so there would be space for all nodes. Suggestion: minimum 100x100 (this suggestion is actually a mistake, should be 800x800) **(3 points)**

Power lines are drawn as straight lines and if necessary, line should turn only in right angle. Only start and end nodes in power lines are considered. Vertices list is ignored. (These few lines of text are unclear) -> "Only lines which have start and end node where those nodes exist in collection of nodes are drawn. The rest of power lines are ignored. <- (The rest of text is ok) Repeated drawing of lines between two same nodes should be ignored.

**2-a:** Power line is drawn as the shortest path between two nodes (any shorthest path). If a line is already drawn there, don't draw another one over it. If there is an intersection of power lines, mark the intersection. **(4 points)**

**2-b:** A shorthest path WITHOUT intersection with already drawn paths is drawn (BFS algorithm). In second pass, power lines for which there couldn't be found a path without intersection are drawn with markage of intersections. Algorithm should start from some two nodes that have the shorthest distance on grid. Find them automatically or manually. **(6 points)**

**BFS:** Grid should be stored in the form of matrices/array of arrays/list. BFS takes the beginning node and checks if that node is the goal node. Then it takes the children of that node and checks if some of them is the goal node. Children of some node are adjacent row and adjacent column. There should be a list of all nodes that have been processed, so there wouldn't be double the job. If the first child is not the goal node, its children are added to the list of nodes for further process, then continue with the second child and so on. When the goal node is reached, the whole path to that node is returned -> also the list of all processed nodes that led to goal node is stored.
<br/>
Example: https://dzone.com/articles/breadth-first-search-c.
<br/>
<br/>
**Geographical scheme**
<br/>
![Modules](https://i.imgur.com/txZSwMj.png)
<br/>
<br/>
**Network graph**
<br/>
![Modules](https://i.imgur.com/92elubT.png)
