# PhraseFinder
A very quickly thrown together tool that scans a file and reports the amount of occurances of each word, sentence, and word sequences.

# How To Use
Step 1: Unzip to a folder, or build the program yourself if you know how to. You must be on a windows machine, unfortunately. I do not know how to build for Mac.

Since this is very quickly thrown together, you must place a txt file next to the exe and name it EXACTLY (without quotes): 'text.txt' (an example text.txt is already provided. Simply replace it with yours.)

Then, run Steve.exe. A console window should pop up. It will now look for duplicate words, sentences & word sequences (phrases).

The window should tell you its progress. It will continue doing its work until there are no more duplicate phrases to be found (could take a while for big files), or you close the program yourself. 

There are now multiple additional txt files next to the EXE:
- zwords.txt lists all the duplicate words. 
- zsentences.txt lists all the duplicate sentences
- zphrases#.txt list all the phrases of length #

These files are not sorted. You will have to sort the contents yourself by selecting all the contents of a txt file and copy pasting them into eg. Google Sheets and sort it in there.

Tadaa

# Additional notes
All characters are converted to lowercase. Most weird symbols are ignored.

Phrases ignore sentence end and start.

Sentences are separated by ! ? : ; or . (the latter may cause inaccuraties due to certain abbreviations, eg. 'eg.')

