import sys
import os
from gtts import gTTS

argCount = len(sys.argv)
ffmpegLocation = 'C:/Code/GL2019/TTS/ffmpeg.exe'

if argCount != 3:
	print('invalid args')
	sys.exit(1)

text = sys.argv[1]
outDir = sys.argv[2]
outFilenameBase = outDir + text
outFilenameWAV = outFilenameBase + '.wav'
outFilenameMP3 = outFilenameBase + '.mp3'

print('saving ', outFilenameBase)

if os.path.isfile(outFilenameWAV):
	print('file already exists')
	sys.exit(1)

tts = gTTS(text, lang='en')
tts.save(outFilenameMP3)

ffmpegCommand = ffmpegLocation + ' -i "' + outFilenameMP3 + '" "' + outFilenameWAV + '"'
print('running', ffmpegCommand)
os.system(ffmpegCommand)
