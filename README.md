# SpotifyStatus

A simple application that reads the currently playing song from spotify and outputs it to files (to be shown in OBS) or standard out (for use in Mix It Up).
 
## Downloading
You can download this application from the releases page [here](https://github.com/ScottHarwoodTech/SpotifyStatus/releases/latest)

1. Go to the releases page: [here](https://github.com/ScottHarwoodTech/SpotifyStatus/releases/latest)
2. Download the "SpotifyStatus.zip" file
3. Extract all files to a safe place

## Running warning
When you run the executable you will get a Windows Defender Smart Screen warning because the executable is not signed. They are not signed because request signing is $500 a year and I'm not made of money. You can click "more-info" then "run anyway". 

If you are uncomfortable I suggest downloading the source code and compiling it from source yourself.

## Ensure you have the valid runtime installed

This requires .net 8.0 runtime installed which can be downloaded from here: 

https://dotnet.microsoft.com/en-us/download/dotnet/8.0/runtime?cid=getdotnetcore&os=windows&arch=x64 

Ensure you download x64 for "run console applications"

## Starting

### Stdout Mode
Double click on the `SpotifyStatus.exe` file to run the program in `stdout` mode which means it will output the Artist and Song and instantly quit


### File Mode
Double Click on the `FileMode.bat` file to run the program in `File` mode which means it will out put the artist and song to local files once a second

## Exiting 

You can exit the application by pressing `control+c` this will automatically exit the application

## Setting Options

You can set the custom options such as:

- `tile-file` Which file to write the title of the song to
- `artist-file` Which file to write the artist of the song to
- `output-file` Which file to write the full song and title to a single file
- `refresh-rate` How frequently to update the song (defaults to one second)
- `postfix` What to post after the text in each file (defaults to " ")
- `prefix` What to post before the text in each file (defaults to "")


These options can be changed in `FileMode.bat` (right click then `edit`) to point to custom files they can also be passed to the executable as arguments like so:

`SpotifyStatus.exe --title-file="./title.txt" --artist-file="./artist.txt" --refresh-rate=2 --output-file="./now-playing.txt" --prefix="" --postfix=" "`

## OBS


![OBS](images/OBS.png)


You can reference the files updated by SpotifyStatus as text components in OBS. 

In OBS:

1. Add Source
2. Text (GDI+)
3. Set title to `Spotify Artist`
4. Click `ok`
5. Check `Read From File` 
6. Browse to your artist.txt file (defaults to the location where you unzipped Spotify Status)
7. Scale and place as desired

1. Add Source
2. Text (GDI+)
3. Set title to `Spotify Title`
4. Click `ok`
5. Check `Read From File` 
6. Browse to your title.txt file (defaults to the location where you unzipped Spotify Status)
7. Scale and place as desired

## Mix It Up

You can invoke the SpotifyStatus.exe as an external executable within Mix It Up to return the current song title and artist for example using as a `!song` command. 

### !song command example

#### Import Actions
1. Commands -> New Command
2. Set the name to `Now Playing`
3. Set the triggers to `song`
4. Import the actions from `song.miucommand`
5. Open the `Get Song Title` action
6. Browse to where you have extracted `SpotifyStatus.exe` locally
7. Select `SpotifyStatus.exe`

#### Manual setup
1. Commands -> New Command
2. Set the name to `Now Playing`
3. Set the triggers to `song`
4. Add a `external program` action
5. Browse to where you have extracted `SpotifyStatus.exe` locally
6. Select `SpotifyStatus.exe`
7. Check `Wait Until Complete` and `Save Output`
8. Add a `Chat Message` Action
9. Set the message to `Currently Playing: $externalprogramresult`
10. Click the play button to test

## Auto Start When Stream Starts

It would be nice to automatically start `SpotifyStatus` when you start streaming, we can accomplish this through Mix It Up
#### Import Actions

#### Import Actions
1. Events -> Generic 
2. Application Launch
3. Click `Add Command`
4. Import the actions from `autostart.miucommand`
5. Open the `Launch Spotify Status` action
6. Browse to where you have extracted `SpotifyStatus.exe` locally
7. Select `SpotifyStatus.exe`

#### Manual setup
1. Events -> Generic 
2. Application Launch
3. Click `Add Command`
4. Add an `External Progam` action
5. Browse to where you have extracted `SpotifyStatus.exe` locally
6. Select `SpotifyStatus.exe`
7. Set the program arguments to what is stored in `FileMode.bat` or set use the defaults provided
8. Click `Show Window` so that you can close the program when you stop streaming