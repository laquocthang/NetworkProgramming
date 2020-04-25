[![NuGet Version](https://img.shields.io/nuget/v/SharpPcap.svg?style=flat-square&label=NuGet&logo=nuget)](https://www.nuget.org/packages/SharpPcap/)
[![AppVeyor CI builds](https://img.shields.io/appveyor/ci/chmorgan/sharppcap/master.svg?style=flat-square&label=AppVeyor&logo=appveyor)](https://ci.appveyor.com/project/chmorgan/sharppcap/branch/master)
[![Build Status](https://dev.azure.com/chmorgan/chmorgan/_apis/build/status/chmorgan.sharppcap?branchName=master)](https://dev.azure.com/chmorgan/chmorgan/_build/latest?definitionId=1&branchName=master)
[![CircleCI](https://circleci.com/gh/chmorgan/sharppcap.svg?style=svg)](https://circleci.com/gh/chmorgan/sharppcap)
[![Travis-CI](https://travis-ci.com/chmorgan/sharppcap.svg?branch=master)](https://travis-ci.com/chmorgan/sharppcap)
[![codecov](https://codecov.io/gh/chmorgan/sharppcap/branch/master/graph/badge.svg)](https://codecov.io/gh/chmorgan/sharppcap)
[![Gitter](https://badges.gitter.im/SharpPcap/community.svg)](https://gitter.im/SharpPcap/community?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge)

# sharppcap
Fully managed, cross platform (Windows, Mac, Linux) .NET library for capturing packets from live and file based devices

The official SharpPcap repository.

# Features
Note that packet dissection and creation was split from SharpPcap some years ago into a separate project, [Packet.Net](https://github.com/chmorgan/packetnet). See the Packet.Net page for a full list of supported packet formats.

* On Linux, support for [libpcap](http://www.tcpdump.org/manpages/pcap.3pcap.html)

* On Windows, support for:
  * Npcap (formerly WinPcap) extensions, see [Npcap API guide](https://nmap.org/npcap/guide/npcap-devguide.html#npcap-api)

* Live device lists
* Statistics
* Reading packets from Live Devices (actual network devices) and Offline Devices (Capture files)
* Support for Berkley Packet Filters
* Dumping packets to Pcap files.

# Examples
See the [Examples](https://github.com/chmorgan/sharppcap/tree/master/Examples) folder for a range of examples using SharpPcap

# CI support
We have support for a number of CI systems for a few reasons:

* Diversity of CI systems in case one of them shuts down
* Examples in case you'd like to customize SharpPcap and make use of one of these CI systems for internal builds. Note that we assume you are following the license for the library.

# Releases
SharpPcap is released via nuget

