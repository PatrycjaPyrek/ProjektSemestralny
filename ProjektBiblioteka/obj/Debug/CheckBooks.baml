   M S B A M L   `   `   `  ����L  HProjektBiblioteka, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:ProjektBibliotekaProjektBiblioteka  5   	   "    ProjektBiblioteka.CheckBooks   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   1localclr-namespace:ProjektBiblioteka     5   	   $	��600��5      $
��1080��6'     � WindowStartupLocation$  Manual=�64   $y�Visible=�6S    � WindowStyle$
 None=�6h   
����������	#FFF7F7F7 � 
ResizeMode$ NoResize=�6�   .��5
      �� ���	#FF232020._�5   
   �  J� 	AllowDrop . $	��600��6    $��Bottom=�6-   $��Right=�6H   $
��1080��6d   
������� � ColumnDefinitions 5      �� 5      $	��730 �6#   �� 5      $	��350 �6#   .+�5      � $��70��6   $	��Top=�6   ���	#FF13306E$��2��6N    5      �� 5      $	��730 �6'   �� 5      $
��350* �6'   .+�5      d� -   5         d�Name$ 
UserButton��5      $��30��63   $��30��6>   $��0q�6J   $��10,15,680,15q�6^    � ToolTip 5      _� 5       _� 	Placement$ Mouse=�6"   .��65   Login using different account6T    � ContextMenu 5      �� 5      -   5        	 ��Name$	 contentMenu��5   &   .@�5      m� $��Log Out��6'   m� 5      $��New User��6'   m� 5      $��Delete User��6'   5       ��5#      �� 5%      
 �� Triggers
 5'      P� 5(   "    P� Property �@J�IsMouseOver P� Value . . �5)   &   ��  �� Property � �� �� Value
 �   �6J   5*   #   5,      ��5.      �� 50       �� ImageSource$ pics/logoCzl.png��6%    j� Stretch$
 Fill=�6D   53      �� 54      $��LIBRARY APP ��6   $��Center=�60   $��Bottom=�6M   $��24(�6h   $��Yu Gothic UI Semibold)�6v   
�������$��1��6�   $��93,0,100,25q�6�   $	��157��6�   $��30��6�   �� 55      -   5        $��Close��55      $��250,18,-140,0q�6)   $	��Top=�6@   $��27��6X   ���	#FF1A2161���	#FFE2DDDD
�������$��14(�6�   $
��Bold"�6�   $��	Agency FB)�6�   $��3��6�   $
��Left=�6  $��74��6  56      � 57      $	��730��6   $��Right=�6   
�������$	��525��6N   $	��Top=�6[   $��0,75,0,0q�6s   .+�5;      � $��67,10,67,428q�6   .+�5<      �� $��LIBRARY APPLICATION��6   $
��Left=�6:   $��92��6U   $	��Top=�6a   $	��555��6y   ���	#FF0F2257$��44(�6�    J� RenderTransformOrigin$ 
0.494,0.52&�6�   $��	ExtraBold"�6�   �� 5=      $��
0,55,430,0q�6    ���	#FFBD5656���	#FF182253$��
ExtraBlack"�6b   6|   �� 5?      -   5         ��Name$ wprowadzenie��5?       $
��Left=�66   $��
0,97,0,-78q�6Q   $
��Wrap=�6e   $	��Top=�6y   $��68��6�   $	��593��6�    �� TextAlignment$ Center=�6�   ���	#FF46494F$��20(�6�   �� 6�   $h�bHere You can see all the books in our library. Books that are currently not available are shaded. ��6�   6W  �� 5@      $��10,38,430,144q�6    ���	#FFBD5656���	#FF182253$��
ExtraBlack"�6e   6   5C      �� 5F      -   5         ��Name$ 	booksList��5F      $
��Left=�6-   $	��250��6H   $��
67,197,0,0q�6U   $	��Top=�6i   $	��596��6�   $��14(�6�   $
��Bold"�6�   6�   �� 5G      -   5         ��Name$ checkByAuthor��5G      $��Check by Author��60   $
��Left=�6J   $��
24,472,0,0q�6e   $	��Top=�6y   $	��118��6�   $��24��6�   6�   �� 5H      -   5        $ Authors��5H      $
��Left=�6+   $��24��6F   $��147,472,0,0q�6R   $	��Top=�6g   $	��196��6   �� 5I      -   5        $ 	AllTitles��5I      $��Check all titles��6,   $
��Left=�6G   $��568,168,0,0q�6b   $	��Top=�6w   $��95��6�   $��24��6�   6�   �� 5J      -	   5        $ checkByGenre��5J      $��Check by Genre��6/   $
��Left=�6H   $��366,472,0,0q�6c   $	��Top=�6x   $	��118��6�   $��24��6�   6�   �� 5K      -
   5        $ Genres��5K      $
��Left=�6*   $��24��6E   $��494,472,0,0q�6Q   $	��Top=�6f   $	��196��6~   5L      5N      �� 5O      ���	#FF134E89 �� CornerRadius$ 10��6,   $��10,80,10,10q�6>   $��1��6S   ._�5P      �� -   5         ��Name$ panel��5P      $��1��6,   $��Center=�6<   $	��455��6Y   $��
14,15,-4,5q�6f   $��Center=�6z   $	��320��6�   .+�5Q      � $	��305��6   $��Right=�6'   $	��530��6C   $	��Top=�6P   $��0,0,15,0q�6h   .+�5S      � $��67,10,67,352q�6   .+�5U      A� 
�������$��0,0,0,8q�63   ���	#FF12427C$��4��6W   �� 5V      $��図
書
館��6$   $
��Left=�6<   $	��160��6W   $��61,8,0,0q�6d   $	��Top=�6v   $��81��6�   $��UD Digi Kyokasho N-B)�6�   ���	#FF182B61$��40(�6�   $ 
0.494,0.52&�6�   5X      5Z      �� 5^      -   5        $��Borrow��5^      $
��Left=�6.   $��69��6I   $��10,-280,0,0q�6U   $	��Top=�6j   $	��286��6�   ���	#FF12427C���	#FF12427C���	#FFFDFDFD$��24(�6�   $��Yu Gothic Light)�6�   �� 5_      -   5        $��Check��5_      $
��Left=�6-   $��69��6H   $��10,-355,0,0q�6T   $	��Top=�6i   $	��286��6�   ���	#FF12427C���	#FF12427C���	#FFFDFDFD$��24(�6�   $��Yu Gothic Light)�6�   �� 5`      -   5        $��Return��5`      $
��Left=�6.   $��69��6I   $��10,-200,0,0q�6U   $	��Top=�6j   $	��286��6�   ���	#FF12427C���	#FF12427C���	#FFFDFDFD$��24(�6�   $��Yu Gothic Light)�6�   5a      5b      5c      5d      5f      