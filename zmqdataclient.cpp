#include <iostream>
#include <string>

#include "zmq_helpers.hpp"

#include "ports.h"
#include "processdata.pb.h"

using namespace std;

int main(void) {
	cout << "Starting the Client..." << endl;

	GOOGLE_PROTOBUF_VERIFY_VERSION;

	energy::Status status;

	zmq::context_t zmqContext( 1);

	string serverAddress = "tcp://localhost:" + to_string( mainPort);
	zmq::socket_t clientSocket( zmqContext, ZMQ_DEALER);
	clientSocket.connect( serverAddress.c_str());

	int i = 0;

	while( i++ < 10000) {
		sendMultipartZmqMessage( clientSocket, "");
		sendZmqMessage( clientSocket, "i am working " + to_string( i));

		string receivedMsg = receiveZmqMessage( clientSocket);
		cout << "server says: " << receivedMsg << endl;
		receivedMsg = receiveZmqMessage( clientSocket);

		cout << "server says: " << receivedMsg << endl;

		struct timespec t;
		t.tv_sec = 0;
		t.tv_nsec = 2 * 1000000;
		nanosleep (&t, NULL);
	}
	return EXIT_SUCCESS;
}
