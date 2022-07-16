#!/bin/bash
release="prototype"
version="0.01"

butler push .export/Linux the1krutz/godot_horde_defense:linux-$release --userversion $version
butler push .export/Windows the1krutz/godot_horde_defense:windows-$release --userversion $version
butler push .export/HTML the1krutz/godot_horde_defense:HTML5-$release --userversion $version