rm -rf .export/*

mkdir -p .export/Linux
../Godot_v3.4.4-stable_mono_x11_64/Godot_v3.4.4-stable_mono_x11.64 --export "Linux/X11" ".export/Linux/GodotSpace.x86_64" --no-window

mkdir -p .export/Windows
../Godot_v3.4.4-stable_mono_x11_64/Godot_v3.4.4-stable_mono_x11.64 --export "Windows Desktop" ".export/Windows/GodotSpace.exe" --no-window

mkdir -p .export/HTML
../Godot_v3.4.4-stable_mono_x11_64/Godot_v3.4.4-stable_mono_x11.64 --export "HTML5" ".export/HTML/GodotSpace.html" --no-window

cp .export/HTML/GodotSpace.html .export/HTML/index.html -u