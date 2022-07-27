#!/bin/bash
release="prototype"
version="0.02"

butler push .export/Linux the1krutz/horde-defense:linux-$release --userversion $version
butler push .export/Windows the1krutz/horde-defense:windows-$release --userversion $version
butler push .export/HTML the1krutz/horde-defense:HTML5-$release --userversion $version