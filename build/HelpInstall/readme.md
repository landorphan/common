Both doxygen and SHFB fail to document xml comments that reference certain nuget packages.  For example, System.Collections.Immutable.  Apparently, the documentation files of many Microsoft nuget packages contain malformed xml.



Rather than finding, fixing, and manually maintaining XML doc files from Microsoft Nuget packages, I am trying out DocFx