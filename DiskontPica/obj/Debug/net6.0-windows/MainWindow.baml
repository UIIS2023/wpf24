   M S B A M L   `   `   `  ����F  BDiskontPica, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:DiskontPicaDiskontPica  5   	       DiskontPica.MainWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:DiskontPica     5   	     � Title$  DISKONT PICA��5   	   $	��450��6   $	��800��6+   .��5	      � .+�5
   
   �� -   5        $��btnKupac��5
      $��Kupac��6!   $
��Left=�61   $��	30,91,0,0q�6L   $	��100��6_   $	��Top=�6k   $��Arial)�6�   �� 5   
   -   5        $��btnZaposleni��5      $��	Zaposleni��6%   $
��Left=�69   $��
30,134,0,0q�6T   $	��100��6h   $	��Top=�6t   $��Arial)�6�   �� 5   
   -   5        $��btnDobavljac��5      $��	Dobavljac��6%   $
��Left=�69   $��
30,178,0,0q�6T   $	��100��6h   $	��Top=�6t   $��Arial)�6�   �� 5   
   -   5        $��btnNarudzbina��5      $��Narudžbina��6&   $
��Left=�6;   $��
663,80,0,0q�6V   $	��100��6j   $	��Top=�6v   $��Arial)�6�   �� 5   
   -   5        $��btnKupovina��5      $��Kupovina��6$   $
��Left=�67   $��663,112,0,0q�6R   $	��100��6g   $	��Top=�6s   $��Arial)�6�   �� 5   
   -   5        $��btnRacun��5      $��Račun��6!   $
��Left=�61   $��662,192,0,0q�6L   $	��100��6a   $	��Top=�6m   $��Arial)�6�   �� 5   
   -   5        $��
btnFaktura��5      $��Faktura��6#   $
��Left=�65   $��662,223,0,0q�6P   $	��100��6e   $	��Top=�6q   $��Arial)�6�   �� 5   
   -   5        $��	btnZalihe��5      $��Zalihe��6"   $
��Left=�63   $��663,286,0,0q�6N   $	��100��6c   $	��Top=�6o   $��Arial)�6�   �� 5   
   -	   5        $��btnPica��5      $
��Pica��6    $
��Left=�6/   $��
30,253,0,0q�6J   $	��100��6^   $	��Top=�6j   $��Arial)�6�   �� 5   
   -
   5        $��btnVrstaPica��5      $��
Vrste pica��6%   $
��Left=�6:   $��
30,288,0,0q�6U   $	��100��6i   $	��Top=�6u   $��Arial)�6�   �� 5   
   -   5        $��btnDODAJ��5      $��DODAJ��6!   $
��Left=�61   $��137,376,0,0q�6L   $	��100��6a   $	��Top=�6m   $��18(�6�   $
��Bold"�6�   $��Arial)�6�   �� 5   
   -   5        $��	btnIZMENI��5      $��IZMENI��6"   $��Center=�63   $��	0,376,0,0q�6P   $	��100��6c   $	��Top=�6o   $��18(�6�   $
��Bold"�6�   $��Arial)�6�   �� 5   
   -   5        $��	btnOBRISI��5      $��OBRIŠI��6"   $
��Left=�63   $��569,376,0,0q�6N   $	��100��6c   $	��Top=�6o   $��18(�6�   $
��Bold"�6�   $��Arial)�6�   &   System.Windows.Controls.DataGrid  5   
   -   5        $��dataGridCentralni��5      $��Center=�6,   $	��251��5      $	��Top=�6    $	��442��68      AlternatingRowBackground
 � ���   
IsReadOnly$
 true��6e      CanUserAddRows$ False��6w      SelectionUnit$ FullRow=�5         SelectionMode$ Single=�6+   $��0,66,0,0q�6B   �� 5   
   $��DISKONT PICA��6   $��Center=�6'   $��0,10,0,0q�6D   $	��160��6V   $	��Top=�6c   $��20(�6{   $
��Bold"�6�   $��Arial)�6�   �� 5   
   
���   �$��1q�6%   $
��Left=�69   $	��144��6T   $��	10,66,0,0q�6a   $	��Top=�6t   $	��141��6�   �� 5   
   
���   �$��1q�6%   $
��Left=�69   $��82��6T   $��
10,235,0,0q�6`   $	��Top=�6t   $	��141��6�   �� 5   
   
���   �$��1q�6%   $
��Left=�69   $��76��6T   $��
650,71,0,0q�6`   $	��Top=�6t   $	��126��6�    J� RenderTransformOrigin$ 0.485,0.519&�6�   �� 5   
   
���   �$��1q�6%   $
��Left=�69   $��89��6T   $��	650,0,0,0q�6`   $��Center=�6s   $	��128��6�   �� 5   
   
���   �$��1q�6%   $
��Left=�69   $��41��6T   $��650,276,0,0q�6`   $	��Top=�6u   $	��127��6�   5       5!      