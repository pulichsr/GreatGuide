cls
echo CarApplication.WM5
echo
echo
echo
echo
del *.log

"C:\Program Files\Microsoft Visual Studio 8\Common7\IDE\devenv" /rebuild "Release" "D:\Projects\GoodGuide\Tech\GoodGuide\CarApplication.WM5.sln"  > CarApplication.Rebuild.log

del /Q "D:\Projects\GoodGuide\Tech\GoodGuide\Dll.WM5\*.xml" 
del /Q "D:\Projects\GoodGuide\Tech\GoodGuide\Dll.WM5\*.pdb" 

del /Q "D:\Projects\GoodGuide\Tech\GoodGuide\Dll\CarApplication.WM5\*.xml" 
del /Q "D:\Projects\GoodGuide\Tech\GoodGuide\Dll\CarApplication.WM5\*.pdb" 

