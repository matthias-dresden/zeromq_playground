#include <iostream>
#include <string>

#include "zmq_helpers.hpp"

#include "ports.h"
#include "processdata.pb.h"

using namespace std;

int main(void) {
	cout << "Starting the Server..." << endl;

	GOOGLE_PROTOBUF_VERIFY_VERSION;

	energy::Status status;

	zmq::context_t zmqContext( 1);

	string serverAddress = "tcp://*:" + to_string( mainPort);
	zmq::socket_t serverSocket( zmqContext, ZMQ_ROUTER);
	serverSocket.bind( serverAddress.c_str());

	int i = 0;

	while( i++ < 10000) {
		std::string identity = receiveZmqMessage( serverSocket);
		cout << "received identity " << identity << endl;
		for( int k = 0; k< 2; k++) {
			cout << "now receiving part " << k << endl;
			string receivedMsg = receiveZmqMessage( serverSocket);
			cout << "received part " << k <<": " << receivedMsg << endl;
		}

		sendMultipartZmqMessage( serverSocket, identity);
		sendMultipartZmqMessage( serverSocket, "");
		sendZmqMessage( serverSocket, "work harder");

	}
	return EXIT_SUCCESS;
}
