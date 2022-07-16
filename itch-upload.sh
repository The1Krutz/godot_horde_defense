#!/bin/bash
release="prototype"
version="0.02"

butler push .export/Linux the1krutz/godotspace:linux-$release --userversion $version
butler push .export/Windows the1krutz/godotspace:windows-$release --userversion $version
butler push .export/HTML the1krutz/godotspace:HTML5-$release --userversion $version