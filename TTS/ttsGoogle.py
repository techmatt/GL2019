import sys
import os
from gtts import gTTS

argCount = len(sys.argv)

if argCount != 3:
	print('invalid args')
	sys.exit(1)

text = sys.argv[1]
outDir = sys.argv[2]
outFilename = outDir + text + '.wav'
print('saving ', outFilename)

if os.path.isfile(outFilename):
	print('file already exists')
	sys.exit(1)

tts = gTTS(text, lang='en')
tts.save(outFilename)
