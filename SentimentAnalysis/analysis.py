import json
from nltk.sentiment.vader import SentimentIntensityAnalyzer

def analyze_sentiment(sentence):
    print(sentence + "\n")
    sid = SentimentIntensityAnalyzer()
    ss = sid.polarity_scores(sentence)
    for k in sorted(ss):
        print('{0}: {1}, '.format(k, ss[k]), end='')
    print()

#sentence = json.loads('{"ID":"2e1b474f-1f40-453f-bf29-35313daf5b02","Name":"Tonio","Email":"antrad@libero.it","FeedbackDate":"2018-02-18T18:02:28.249322+02:00","Text":"This site sux!"}')

#analyze_sentiment(sentence["Text"])


