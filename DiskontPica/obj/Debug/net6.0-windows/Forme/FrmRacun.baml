   M S B A M L   `   `   `  ����F  BDiskontPica, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:DiskontPica.FormeDiskontPica.Forme  5   	        DiskontPica.Forme.FrmRacun   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   1localclr-namespace:DiskontPica.Forme     5   	     � Title$  Racun��5   	   $	��450��6   $	��377��6$   .��5	      � 
�������.+�5
   
   �� $��UNOS/IZMENA RACUNA:��6   $
��Left=�6.   $��	70,50,0,0q�6I   $	��Top=�6\   $��18(�6t   $��Arial)�6�   $
��Bold"�6�   �� 5   
   $��Datum��6   $
��Left=�6    $��	70,95,0,0q�6;   $	��Top=�6N   $��16(�6f   $��Right=�6t   $��92��6�   $��Arial)�6�   �� 5   
   $��Suma za uplatu��6   $
��Left=�6)   $��
39,147,0,0q�6D   $	��Top=�6X   $��16(�6p   $��Right=�6~   $	��123��6�   $��Arial)�6�   �� 5   
   $��Valuta��6   $
��Left=�6!   $��	109,0,0,0q�6<   $��Center=�6O   $��16(�6j   $��Right=�6x   $��Arial)�6�   �� 5   
   $��Kupac��6   $
��Left=�6    $��
70,252,0,0q�6;   $	��Top=�6O   $��16(�6g   $��Right=�6u   $��92��6�   $��Arial)�6�   �� 5   
   $��	Zaposleni��6   $
��Left=�6$   $��
70,302,0,0q�6?   $	��Top=�6S   $��16(�6k   $��Right=�6y   $��92��6�   $��Arial)�6�   �� 5   
   -   5        $��txtSuma��5      $
��Left=�6!   $��175,150,0,0q�6<    �� TextWrapping$
 Wrap=�6Q   $�� ��6e   $	��Top=�6m   $	��120��6�   $��16(�6�   $��Arial)�6�   �� 5   
   -   5        $��	txtValuta��5      $
��Left=�6#   $��	175,0,0,0q�6>   $
 Wrap=�6Q   $�� ��6e   $��Center=�6m   $	��120��6�   $��16(�6�   $��Arial)�6�   �� 5   
   -   5        $��
btnSacuvaj��5      $��SAČUVAJ��6#   $
��Left=�65   $��
70,357,0,0q�6P   $	��Top=�6d   $	��100��6|   $��18(�6�   $��Arial)�6�   $
��Bold"�6�   
�������6�   �� 5   
   -   5        $��	btnOtkazi��5      $��OTKAŽI��6"   $��right=�63   $��175,357,75,0q�6O   $	��Top=�6e   $	��100��6}   $��18(�6�   $��Arial)�6�   $
��Bold"�6�   
�������6�   �� 5   
   -   5        $��cbKupac��5      $
��Left=�6"   $��177,257,0,0q�6=   $	��Top=�6R   $	��120��6j    J� RenderTransformOrigin$ 0.594,2.006&�6v    �� DisplayMemberPath$ Kupac��6�    �� SelectedValuePath$ id_kupca��6�   $��Arial)�6�   �� 5   
   -   5        $��cbZaposleni��5      $
��Left=�6&   $��175,308,0,0q�6A   $	��Top=�6V   $	��120��6n   $ 	Zaposleni��6z   $ id_zaposleni��6�   $��Arial)�6�   (  "System.Windows.Controls.DatePicker  5   
   -   5        $��dpDatum��5      $
��Left=�6$   $��179,102,0,0q�6?   $	��Top=�6T   $	��116��6l   $��Arial)�6x   5      5      