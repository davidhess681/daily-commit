package main

import (
	"fmt"

	"example.com/greetings"
	// "rsc.io/quote"
)

func main() {
	// fmt.Println(quote.Go())
	message := greetings.Hello("David")
	fmt.Println(message)
}
