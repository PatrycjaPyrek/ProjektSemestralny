   M S B A M L   `   `   `  ����L  HProjektBiblioteka, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:ProjektBibliotekaProjektBiblioteka  5   	   "    ProjektBiblioteka.ReturnBook   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   1localclr-namespace:ProjektBiblioteka     5   	   $	��600��5      $
��1080��6'     � WindowStartupLocation$  Manual=�64   $y�Visible=�6S    � WindowStyle$
 None=�6h   
����������	#FFF7F7F7 � 
ResizeMode$ NoResize=�6�   .��5
      �� ���	#FF232020._�5   
   �  J� 	AllowDrop . $	��600��6    $��Bottom=�6-   $��Right=�6H   $
��1080��6d   
������� � ColumnDefinitions 5      �� 5      $	��730 �6#   �� 5      $	��350 �6#   .+�5      � $��70��6   $	��Top=�6   ���	#FF13306E$��2��6N    5      �� 5      $	��730 �6'   �� 5      $
��350* �6'   .+�5      d� -   5         d�Name$ 
UserButton��5      $��30��63   $��30��6>   $��0q�6J   $��10,15,680,15q�6^    � ToolTip 5      _� 5       _� 	Placement$ Mouse=�6"   .��65   Login using different account6T    � ContextMenu 5      �� 5      -   5        	 ��Name$	 contentMenu��5   &   .@�5      m� $��Log Out��6'   m� 5       $��New User��6'   m� 5!      $��Delete User��6'   5"      ��5$      �� 5&      
 �� Triggers
 5(      P� 5)   "    P� Property �@J�IsMouseOver P� Value . . �5*   &   ��  �� Property � �� �� Value
 �   �6J   5+   #   5-      ��5/      �� 51       �� ImageSource$ pics/logoCzl.png��6%    j� Stretch$
 Fill=�6D   54      �� 55      $��LIBRARY APP ��6   $��Center=�60   $��Bottom=�6M   $��24(�6h   $��Yu Gothic UI Semibold)�6v   
�������$��1��6�   $��93,0,100,25q�6�   $	��157��6�   $��30��6�   �� 56      -   5        $��Close��56      $��250,18,-140,0q�6)   $	��Top=�6@   $��27��6X   ���	#FF1A2161���	#FFE2DDDD
�������$��14(�6�   $
��Bold"�6�   $��	Agency FB)�6�   $��3��6�   $
��Left=�6  $��74��6  57      � 58      $	��730��6   $��Right=�6   
�������$	��525��6N   $	��Top=�6[   $��0,75,0,0q�6s   .+�5;      �� ���	#FFA5C6E2 J� OpacityMask �	#FF5074B4 �� CornerRadius$ 10��6H   $	��136��6[   $��	69,0,69,0q�6h   ._�5<      �� $
��Left=�6    $	��136��6;   $��-1,164,-3,-164q�6H   $	��Top=�6`   $	��596��6x   5=      � 5@      $��67,10,67,428q�6   .+�5A      �� $��LIBRARY APPLICATION��6   $
��Left=�6:   $��92��6U   $	��Top=�6a   $	��555��6y   ���	#FF0F2257$��44(�6�    J� RenderTransformOrigin$ 
0.494,0.52&�6�   $��	ExtraBold"�6�   �� 5B      $��
0,55,430,0q�6    ���	#FFBD5656���	#FF182253$��
ExtraBlack"�6b   �� 5D      -   5         ��Name$ wprowadzenie��5D       $
��Left=�66   $��
0,87,0,-68q�6Q   $
��Wrap=�6e   $	��Top=�6y   $��68��6�   $	��593��6�    �� TextAlignment$ Center=�6�   ���	#FF46494F$��20(�6�   �� 6�   $��zHere you can return borrowed books. Fill in the form. Any extra costs will be displayed in the lower portion of the grid. ��6�   6o  �� 5E      $��10,38,430,144q�6    ���	#FFBD5656���	#FF182253$��
ExtraBlack"�6e   5H      �� 5I      -   5         ��Name$ 	LibraryId��5I      $
��Left=�6-   $��25��6H   $��220,242,0,0q�6T    �� TextWrapping$
 Wrap=�6i   $�� ��6}   $	��Top=�6�   $	��204��6�   �� 5J      $��
Library id��6   $
��Left=�6-   $��25��6H   $��103,238,0,0q�6T   $	��Top=�6i   $	��109��6�   �� 5K      -   5        $ BookId��5K      $
��Left=�6*   $��25��6E   $��220,286,0,0q�6Q   $
 Wrap=�6f   $�� ��6z   $	��Top=�6�   $	��204��6�   �� 5L      $��Book id��6   $
��Left=�6*   $��25��6E   $��103,282,0,0q�6Q   $	��Top=�6f   $	��109��6~   �� 5M      -   5         ��Name$ submitBorrow��5M      $��Submit��6/   $
��Left=�6@   $��30��6[   $��478,262,0,0q�6g   $	��Top=�6|   $	��109��6�   ���	#FF0F2257
�������$
��Bold"�6�   6�   �� 5N      $��Return:��6   $
��Left=�6*   $��38��6E   $��
98,199,0,0q�6Q   $	��Top=�6e   $��95��6}   $��22(�6�   ���	#FF0F2257$
��Bold"�6�   �� 5P      -   5         ��Name$ borderBorrowed��5P      ���	#FFA5C6E2 �	#FF5074B4$ 10��6`   $��67,351,69,10q�6r   ._�5Q      �� $
��Left=�6!   $	��153��6<   $��
67,350,0,0q�6I   $	��Top=�6]   $	��596��6u   5R      �� 5S      -	   5         ��Name$ ReturnedLabel��5S      $��	Returned:��6/   $
��Left=�6C   $��38��6^   $��
96,350,0,0q�6j   $	��Top=�6~   $	��248��6�   $��22(�6�   ���	#FF0F2257$
��Bold"�6�   �� 5T      $��
Library id��6   $
��Left=�6-   $��25��6H   $��101,393,0,0q�6T   $	��Top=�6i   $��66��6�   ���	#FF213B746�   �� 5U      -
   5        $ LibraryReturnedId��5U      $�� ��63   $
��Left=�6>   $��25��6Y   $��172,393,0,0q�6e   $	��Top=�6z   $��62��6�   �� 5V      $
��Name��6   $
��Left=�6'   $��25��6B   $��251,393,0,0q�6N   $	��Top=�6c   $��60��6{   ���	#FF213B746�   �� 5W      -   5        $ NameReturned��5W      $�� ��6.   $
��Left=�69   $��25��6T   $��316,393,0,0q�6`   $	��Top=�6u   $	��326��6�   �� 5X      $��Book id��6   $
��Left=�6*   $��25��6E   $��101,423,0,0q�6Q   $	��Top=�6f   $��66��6~   ���	#FF213B746�   �� 5Y      -   5        $ BookIdBorrowed��5Y      $�� ��60   $
��Left=�6;   $��25��6V   $��172,423,0,0q�6b   $	��Top=�6w   $��62��6�   �� 5Z      $��Title��6   $
��Left=�6(   $��25��6C   $��251,423,0,0q�6O   $	��Top=�6d   $��60��6|   ���	#FF213B746�   �� 5[      -   5        $ TitleBorrowed��5[      $�� ��6/   $
��Left=�6:   $��25��6U   $��311,423,0,0q�6a   $	��Top=�6v   $	��345��6�   �� 5\      $
��Type��6   $
��Left=�6'   $��25��6B   $��101,448,0,0q�6N   $	��Top=�6c   $��66��6{   ���	#FF213B746�   �� 5]      -   5        $ TypeBorrowed��5]      $�� ��6.   $
��Left=�69   $��25��6T   $��177,448,0,0q�6`   $	��Top=�6u   $	��167��6�   �� 5^      $
��Date��6   $
��Left=�6'   $��25��6B   $��101,478,0,0q�6N   $	��Top=�6c   $��66��6{   ���	#FF213B746�   �� 5_      -   5        $ dateAndTime_label��5_      $�� ��63   $
��Left=�6>   $��Center=�6Y   $��41��6}   $��
71,156,0,0q�6�   $	��Top=�6�   $	��590��6�   $ 0.5,0.5&�6�   $��16(�6�   $
��Bold"�6�   z�5`      V� 5a      .~�5b      �� �� 5c      �� 5d       �� Angle$ 0.152Q�6.   T� 5e      5f      5h      �� 5i      -   5        $ DateBorrowed��5i      $�� ��6.   $
��Left=�69   $��25��6T   $��172,478,0,0q�6`   $	��Top=�6u   $	��336��6�   �� 5j      $��To pay��6   $
��Left=�6)   $��25��6D   $��358,448,0,0q�6P   $	��Top=�6e   $��66��6}   ���	#FF213B746�   �� 5k      -   5        $ ToPay��5k      $�� ��6'   $
��Left=�62   $��25��6M   $��429,448,0,0q�6Y   $	��Top=�6n   $	��193��6�   5m      �� 5n      ���	#FF134E89$ 10��6,   $��10,80,10,10q�6>   $��1��6S   ._�5o      �� -   5         ��Name$ panel��5o      $��1��6,   $��Center=�6<   $	��455��6Y   $��
14,15,-4,5q�6f   $��Center=�6z   $	��320��6�   .+�5p      � $	��305��6   $��Right=�6'   $	��530��6C   $	��Top=�6P   $��0,0,15,0q�6h   .+�5r      � $��67,10,67,352q�6   .+�5t      A� 
�������$��0,0,0,8q�63   ���	#FF12427C$��4��6W   �� 5u      $��図
書
館��6$   $
��Left=�6<   $	��160��6W   $��61,8,0,0q�6d   $	��Top=�6v   $��81��6�   $��UD Digi Kyokasho N-B)�6�   ���	#FF182B61$��40(�6�   $ 
0.494,0.52&�6�   5w      5y      �� 5}      -   5        $��Borrow��5}      $
��Left=�6.   $��69��6I   $��10,-280,0,0q�6U   $	��Top=�6j   $	��286��6�   ���	#FF12427C���	#FF12427C���	#FFFDFDFD$��24(�6�   $��Yu Gothic Light)�6�   �� 5~      -   5        $��Check��5~      $
��Left=�6-   $��69��6H   $��10,-355,0,0q�6T   $	��Top=�6i   $	��286��6�   ���	#FF12427C���	#FF12427C���	#FFFDFDFD$��24(�6�   $��Yu Gothic Light)�6�   �� 5      -   5        $��Return��5      $
��Left=�6.   $��69��6I   $��10,-200,0,0q�6U   $	��Top=�6j   $	��286��6�   ���	#FF12427C���	#FF12427C���	#FFFDFDFD$��24(�6�   $��Yu Gothic Light)�6�   5�      5�      5�      5�      5�      