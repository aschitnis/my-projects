   M S B A M L   `   `   `  ���� O  Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem  C ?Wpf.Test, Version=1.0.1.0, Culture=neutral, PublicKeyToken=null#clr-namespace:Wpf.TestWpf.Test E'clr-namespace:Wpf.Test.Custom.ConverterWpf.Test.Custom.Converter    Wpf.Test.WindowSalesView   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     9system&clr-namespace:System;assembly=mscorlib     (localclr-namespace:Wpf.Test     >
converters'clr-namespace:Wpf.Test.Custom.Converter       � Title$  WindowSalesView��$	��650��$
��1120�� � 	Resources H  BWpf.Test.Custom.Converter.ParametrizedBooleanToVisibilityConverter ,  (ParametrizedBooleanToVisibilityConverter �� 
TargetType �� Property �� Value �� 
TargetType  CountryItemContainerStyle �� Path �� RelativeSource �� AncestorType	 �� Padding
 J� SnapsToDevicePixels �� Triggers P� Property P� Value �� 
TargetName  RegionItemContainerStyle��%  &	        '��      &	     &	 �      �� DefaultListViewStyle��" ��.���  �@J�SnapsToDevicePixels . ��  � ��$
 Auto=���  � ��$
 Auto=���  � �� . ��  � ��$ Center=���  � �� �� " ��.R��� $��Border��$��2q�
������ 
�������._��� .���� �� " ��.���  �@�FocusVisualStyle Z� ��  � �� �� $  HorizontalContentAlignment� �� " ����  � �� �� $ VerticalContentAlignment� �� " ����  � �� �� " ��.R��� $��	BrCountry��#��z��#��z��#��z��#	 z��
 . ._��� #��z��#��z��#
 z
  P�  �@��
IsSelected . . ��� $ 	BrCountry�� � ��
 ��� �P�  � {� .  . ��� # � �� � ���� " ��.���  � ��
 �������  �@�FocusVisualStyle Z� ��  � �� �� $  HorizontalContentAlignment� �� " ����  � �� �� $ VerticalContentAlignment� �� " ����  � �� �� " ��.R��� $��Bd��#��z��#��z��#��z��#	 z��
 . ._��� #��z��#��z��#
 z
 .���  � ColumnDefinitions �� $
��1.5* ��� $
��Auto ��� $
��1.2* ��� $
��Auto ��� $��2* � � RowDefinitions �� $��3* ��� $
��Auto ��� $��* ��� $��40 ��� $��40 �.+��� -   $��	lbRegions��$��0��$��Disabled=�$��2q�$��Stretch=�$��Center=�$��0��#��[  �� SelectedItem �� *CurrentRegion+���� *RegionsList+{��� *EnableRegionsList+����  �� DataType$  {x:Type system:String} ��.R��� 
����  �$��1q�$��2q�._�� $��grdMain�� ��  �� .+��� $��0������ $: 4DataContext.DisplayCountriesForSelectedRegionCommand� �� $ WindowL����� $ Content.Text� �� *Self+���� " ��.���  � ��
 ��ح���  � ��$ 3q� �� Triggers P�  � �� . . ���  � ��
 ��� ����� $��0������ ���� " ��.���  � ��$ 20(���  �@��TextAlignment$ Center=���  � ��$ Center=��� $��1��$��0��$��4,0,4,0q� �� ResizeBehavior$ PreviousAndNext=� �� ResizeDirection$ Columns=�
�������$��3��� $��2��$��0�� � ShowGridLines .  �� $��45 ��� $��9* �.+��� $
��Left=�$��0��$��20(�$��5q�
����  ����� *ChosenRegion+�� -    ��Name$ lbCountries��$��1��$
��Auto=�$��Hidden=�$��Stretch=�#��[ ���� *	Countries+ �� Mode$ TwoWay=� �� *CurrentCountry+y��� *HasCountries+ �� 	Converter# [   �� ConverterParameter$
 true��$ TwoWay=����� " ��.R��� $��border��
������ $��1q�$��Stretch=�
�������$��1q�._��  �� $��* �.+��� $��18(����� �� $��3��$��4,0,4,0q�$��0��$ PreviousAndNext=�$ Columns=�
�������$��3���� $��4��$��0��
�������$��4q��� -    ��Name$ lv��$��4��$��0��$
��Auto=����� *SalesListForCurrentCountry+���� $  {x:Type system:String} ��.R�� 
�������$��0,3,0,0q�$��Stretch=�$��75�� �� $	��100 ��� $	��120 ��� $	��100 ��� $	��100 � �� $��* ��� $��* �.+��� $��2q�$��0��$��0��$��18(�$��Produkttyp:���� $��0��
����  �$��1��$��2q�$��Stretch=�$��18(����� $ ItemType��� $��2q�$��2��$��0��$��18(�$��Verkaufsart:���� 
����  �$��3��$��0��$��2q�$��Stretch=�$��18(����� $ SalesChannel��� $��2q�$��0��$��1��$��18(�$��Datum:���� 
����  �$��1��$��1��$��2q�$��Stretch=�$��18(����� $ 	OrderDate� �� StringFormat$ {0:dd.MM.yyyy}���� $��2q�$��2��$��1��$��18(�$��BestellNr.:���� 
����  �$��3��$��1��$��2q�$��Stretch=�$��18(����� $ OrderId��� 
�������$��1��$��0��$��3��$��2��$��Stretch=�$
 Rows=��� $��1��$��2��$��4,0,4,0q�$ PreviousAndNext=�$ Columns=�
�������$��3��� $��0��$��2�� �� $��* ��� $��* �.+��� $��0�� �� Orientation$ Vertical=�
�������.+��� $��16(�$��8,0,0,0q����� *31CountryDataVM.CurrentCountryDataModel.CountryName+�� $��16(�$��
population��$��8,0,0,0q��� $��16(�$��	continent��$��8,0,0,0q��� $��1��$ Vertical=�
�������.+��� $��16(�$��capital��$��8,0,0,0q��� $��16(�$��currencycode��$��8,0,0,0q��� $��0��$��4��$��5��
��������� $��0��$��5��$��5������ *=;ApplicationMessageSubscriber.MessageStringFromSingletonData+���� .���  � ��$ 18(���  � ��$ 5q� ��  �� Binding �� *HasFileError+ �� Value true.a���  � ��
 �  ����  �� *HasFileError+	 false.a���  � ��
 ��  �