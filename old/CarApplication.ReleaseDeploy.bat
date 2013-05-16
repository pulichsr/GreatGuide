cls
echo ReleaseTest CarApplication.WM5
echo Removing Build folder
rmdir /S /Q "D:\Projects\GoodGuide\Tech\CarApplicationReleases\Deploy\"

mkdir "D:\Projects\GoodGuide\Tech\CarApplicationReleases\Deploy\"
mkdir "D:\Projects\GoodGuide\Tech\CarApplicationReleases\Deploy\Plugins"
mkdir "D:\Projects\GoodGuide\Tech\CarApplicationReleases\Deploy\Resources"
mkdir "D:\Projects\GoodGuide\Tech\CarApplicationReleases\Deploy\Resources\de-DE"
mkdir "D:\Projects\GoodGuide\Tech\CarApplicationReleases\Deploy\Resources\en-US"
mkdir "D:\Projects\GoodGuide\Tech\CarApplicationReleases\Deploy\Resources\fr-FR"
mkdir "D:\Projects\GoodGuide\Tech\CarApplicationReleases\Deploy\Resources\it-IT"
mkdir "D:\Projects\GoodGuide\Tech\CarApplicationReleases\Deploy\Resources\nl-NL"

copy "D:\Projects\GoodGuide\Tech\GoodGuide\Dll\CarApplication.WM5\*.dll" "D:\Projects\GoodGuide\Tech\CarApplicationReleases\Deploy"
copy "D:\Projects\GoodGuide\Tech\GoodGuide\Dll\CarApplication.WM5\*.dal" "D:\Projects\GoodGuide\Tech\CarApplicationReleases\Deploy"
copy "D:\Projects\GoodGuide\Tech\GoodGuide\Dll\CarApplication.WM5\*.gg*" "D:\Projects\GoodGuide\Tech\CarApplicationReleases\Deploy"
copy "D:\Projects\GoodGuide\Tech\GoodGuide\Dll\CarApplication.WM5\CarApplication.WM5.exe" "D:\Projects\GoodGuide\Tech\CarApplicationReleases\Deploy\GreatGuide.exe"
copy "D:\Projects\GoodGuide\Tech\GoodGuide\Dll\CarApplication.WM5\Plugins\*.gg*" "D:\Projects\GoodGuide\Tech\CarApplicationReleases\Deploy\Plugins"
copy "D:\Projects\GoodGuide\Tech\GoodGuide\Database.sqlite\GoodGuide.sqlite" "D:\Projects\GoodGuide\Tech\CarApplicationReleases\Deploy"
copy "D:\Projects\GoodGuide\Tech\CarApplicationReleases\ConfigFiles\*.gbl" "D:\Projects\GoodGuide\Tech\CarApplicationReleases\Deploy"

copy "D:\Projects\GoodGuide\Tech\CarApplicationReleases\Resources\de-DE\*.*" "D:\Projects\GoodGuide\Tech\CarApplicationReleases\Deploy\Resources\de-DE"
copy "D:\Projects\GoodGuide\Tech\CarApplicationReleases\Resources\en-US\*.*" "D:\Projects\GoodGuide\Tech\CarApplicationReleases\Deploy\Resources\en-US"
copy "D:\Projects\GoodGuide\Tech\CarApplicationReleases\Resources\fr-FR\*.*" "D:\Projects\GoodGuide\Tech\CarApplicationReleases\Deploy\Resources\fr-FR"
copy "D:\Projects\GoodGuide\Tech\CarApplicationReleases\Resources\it-IT\*.*" "D:\Projects\GoodGuide\Tech\CarApplicationReleases\Deploy\Resources\it-IT"
copy "D:\Projects\GoodGuide\Tech\CarApplicationReleases\Resources\nl-NL\*.*" "D:\Projects\GoodGuide\Tech\CarApplicationReleases\Deploy\Resources\nl-NL"

