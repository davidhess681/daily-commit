package main

import (
	"fmt"
	"log"

	"example.com/greetings"
	// "rsc.io/quote"
)

func main() {
	log.SetPrefix("greetings: ")
	log.SetFlags(0)

	// fmt.Println(quote.Go())
	// message, err := greetings.Hello("David")
	names := []string{"David", "Maddie", "Katara"}
	messages, err := greetings.Hellos(names)
	if err != nil {
		log.Fatal(err)
	}

	for _, name := range names {
		fmt.Println(messages[name])
	}
}
