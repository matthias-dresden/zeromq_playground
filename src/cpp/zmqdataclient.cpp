#include <iostream>
#include <string>

#include <unistd.h>

#include "zmq_helpers.hpp"

#include "ports.h"
#include "processdata.pb.h"

using namespace std;

energy::Status getRandomStatus( int i) {
	energy::Status status;

	status.set_available( i%1000 < 500 ? true : false);
	status.set_id( to_string( getpid()));
	status.set_powertype( energy::Status_PowerType_GAS);

	return status;
}


int main(int argc, char* argv[]) {
	cout << "Starting the Client..." << endl;

	std::string remoteHost = "localhost";

	if( argc > 1) {
		remoteHost = argv[ 1];
	}

	zmq::context_t zmqContext( 1);

	string serverAddress = "tcp://" + remoteHost + ":" + to_string( mainPort);
	zmq::socket_t clientSocket( zmqContext, ZMQ_DEALER);
	clientSocket.connect( serverAddress.c_str());

	int i = 0;

	while( true) {

		energy::Status status = getRandomStatus( i++);

		sendMultipartZmqMessage( clientSocket, "");
		sendZmqMessage( clientSocket, status.SerializeAsString());

		string receivedMsg = receiveZmqMessage( clientSocket);
		receivedMsg = receiveZmqMessage( clientSocket);

		struct timespec t;
		t.tv_sec = 0;
		t.tv_nsec = 20 * 1000000;
		nanosleep (&t, NULL);
	}
	return EXIT_SUCCESS;
}
