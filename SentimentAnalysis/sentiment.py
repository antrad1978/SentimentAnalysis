#    Spark
from pyspark import SparkContext
#    Spark Streaming
from pyspark.streaming import StreamingContext
#    Kafka
from pyspark.streaming.kafka import KafkaUtils
#    json parsing
import json
#Sentiment Analysis
from analysis import analyze_sentiment

sc = SparkContext(appName="sentiment")
sc.setLogLevel("WARN")

ssc = StreamingContext(sc, 10)

kafkaStream = KafkaUtils.createStream(ssc, 'localhost:2181', 'spark-streaming-sentiment2', {'DataAnalysis':1})

#lines = kafkaStream.map(lambda x: x[1])

data = kafkaStream.map(lambda x: analyze_sentiment(json.loads(x[1])["Text"]))

#lines.pprint()
data.pprint()

ssc.start()
ssc.awaitTermination()
