## State of War (2001) map editor | 蓝色警戒 (2001) 地图编辑器

This editor is fully functional to change everything (useful) in State of War map files, including .edt and .map (and .til in further development).

这个地图编辑器提供了修改蓝色警戒地图的完整功能, 包括.edt文件和.map文件的全部(有意义的)部分. 在后期开发中可能会加入对.til文件的支持.
    
![Sample 1](https://raw.githubusercontent.com/DragoonKiller/StateOfWarMapEditor/master/Documentation/Pictures/scr.png)

### Usage 用法
* Use right mouse button to move your viewport just like in game.  
和游戏里类似, 右键移动视野.
* Move cursor to any text box, then edit with keyboard without clicking.  
将鼠标移动到文本框中, 直接使用键盘输入内容.
* Type your file path in the text box in the top-left corner. Relative path (e.g. "./StateOfWar/Data/Levels/M00.edt") and absolute path (e.g. "D:/StateOfWar/Data/Levels/M00.edt") are available.  
在左上角的文本框中输入你的地图文件路径. 可以使用相对路径(例如"./StateOfWar/Data/Levels/M00.edt")或绝对路径(例如"D:/StateOfWar/Data/Levels/M00.edt").
* JPG and SRF file are allowed to read into background.  背景图片支持 JPG 和 (原版)SRF 文件. 
* Open the global configuration panel by moving cursor to the upper-right corner, then edit values in the same way. You can click left mouse buttom to switch the checkbox.  
把鼠标移动到右上角打开全局修改面板, 用同样的方式修改数值. 在选择框中点击鼠标左键切换选择.
* Left click to select units and buildings. If there are multiple things can be selected at the grid, they will be selected one by one and then repeat.  
鼠标左键单击选取建筑和单位. 如果格子里有多个单位重叠在一起, 鼠标左键点击会循环选择这些单位.
* Edit selected unit's property on the bottom-left panel.  
在左下角可以修改你选择的单位的属性.
* Cancel seleciton by clicking on empty grid.  
想要取消选择, 点击空地即可.
* To drag a unit, press and hold on left mouse button then move your mouse. Hold Shift key to force align unit's coordinates to grids.  
点击鼠标左键不放, 可以拖动单位. 作战单位的坐标以像素计, 建筑的坐标以格子计, 拖动时按住左shift键可以让作战单位坐标对齐格子.
* Press one of number key 0~7 to change the tile property where cursor is pointing. The backquote key '\`' is also used for cleaning the grid.  
按主键盘上的0到7数字键来改变光标指向的地块的属性. 也可以使用反引号按键'\`'(通常在tab键上方, 数字键1的左边)把地块属性设定为全部可通过.
* Save by pressing Ctrl + S.  
按 Ctrl + S 保存.
* other functions and their shortcut 其它功能快捷键
    * [Shift + Esc | Ctrl + Esc | Alt + F4] exit. 退出. 
    * [Shift + B] switch cursor pointing grid hint. 切换光标位置指示.
    * [Shift + M] switch tiles property display. 切换地块属性显示.
    * [Shift + ;] switch cursor coordinates display. 切换光标坐标值显示.
    * [Shift + T] switch taken place display for units. 切换单位的占地指示标志.
    * [Ctrl + C | Shift + C] copy. 复制当前选择.
    * [Ctrl + V | Shift + V] paste. 粘贴当前选择.
    * [Ctrl + X | Shift + X] cut. 剪切当前选择.
    * [Delete | Shift + Tab] remove. 删除选择的单位.
    * [Shift + W] create new building at cursor pointing grid. 在光标处新建建筑.
    * [Shift + E] create new unit at cursor pointing grid. 在光标处新建单位.
    * [Space] Move to center of map. 回到地图中央.
* ![Tiles](https://raw.githubusercontent.com/DragoonKiller/StateOfWarMapEditor/master/Documentation/Pictures/tiles.png)  
从左到右, 各个格子的功能大致如下:

|按键 Button|`0`|`1`|`2`|`3`|`4`|`5`|`6`|`7`|
-|-|-|-|-|-|-|-|-
|坦克可通行 Tanks available|√||√||√||√||
|飞碟和空军可通行 Air units available|√|√|||√|√|||
|可以造炮塔 Turrets available|√||√||||||

### TODO 未完成功能
* Modification for .til file. .til文件修改.
* A better interaction style for .map editing. 更友好的地块属性(.map)修改方式.
* Extract configuration file from Resources to external path. 配置文件提取到程序运行目录而不是编译进Resources文件.


### Bug Report and Feature Request 提Bug, 提需求

Issue it.  

请开issue.

### Contribution 参与开发

Fork this repo directly, commit your change and send a pullrequest to this repository. It's advised to open an issue for specifying what this project needed.

直接fork, 然后提交 pullrequest. 虽然不强制要求, 但是仍然建议先提交一个issue, 说明你想要添加哪些功能, 或者想要修改什么.

### License 协议

This project uses GPL v3 license.

本项目采用 GPL Version 3 开源协议.

Two external library, RadiacUI and StateOfWarMapUtility uses their own license.

本项目的两个外部库: RadiacUI 和 StateOfWarMapUtility 采用其各自的开源协议, 不一定采用 GPL 协议. 
