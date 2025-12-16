package main

import (
	"context"
	"fmt"

	"github.com/segmentio/kafka-go"
)

func main() {
	topic := "test-topic"
	partition := 0
	conn, _ := connect(topic, partition)

	readWithReader(topic, "consumer-through-kafka 1")

	if err := conn.Close(); err != nil {
		fmt.Println("failed to close connection:", err)
	}
}

func connect(topic string, partition int) (*kafka.Conn, error) {
	conn, err := kafka.DialLeader(context.Background(), "tcp",
		"localhost:9092", topic, partition)
	if err != nil {
		fmt.Println("failed to dial leader")
	}
	return conn, err
}

func readWithReader(topic string, groupID string) {
	r := kafka.NewReader(kafka.ReaderConfig{
		Brokers:  []string{"localhost:9092"},
		Topic:    topic,
		MaxBytes: 100,
	})

	for {
		msg, err := r.ReadMessage(context.Background())
		if err != nil {
			break
		}
		fmt.Printf("message at topic/partition/offset %v/%v/%v: %s = %s\n",
			msg.Topic, msg.Partition, msg.Offset, string(msg.Key), string(msg.Value))
	}

	if err := r.Close(); err != nil {
		fmt.Println("failed to close reader:", err)
	}
}
