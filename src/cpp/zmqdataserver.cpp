#include <iostream>
#include <string>

#include "zmq_helpers.hpp"

#include "ports.h"
#include "processdata.pb.h"

using namespace std;

int main(void) {
	cout << "Starting the Server..." << endl;

	energy::Status status;

	zmq::context_t zmqContext( 1);

	string serverAddress = "tcp://*:" + to_string( mainPort);
	zmq::socket_t serverSocket( zmqContext, ZMQ_ROUTER);
	serverSocket.bind( serverAddress.c_str());

	int i = 0;

	while( true) {
		std::string identity = receiveZmqMessage( serverSocket);
		string envelope = receiveZmqMessage( serverSocket);

		string payload = receiveZmqMessage( serverSocket);
		energy::Status status;
		status.ParseFromString( payload);


		sendMultipartZmqMessage( serverSocket, identity);
		sendMultipartZmqMessage( serverSocket, "");
		sendZmqMessage( serverSocket, "work harder");

		i++;

		if( i%10000 == 0) {
			cout << "received " << i << " messages" <<endl;
		}

	}
	return EXIT_SUCCESS;
}
