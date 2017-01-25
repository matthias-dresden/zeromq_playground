#ifndef __ZHELPERS_HPP_INCLUDED__
#define __ZHELPERS_HPP_INCLUDED__

#include <zmq.hpp> // https://github.com/zeromq/cppzmq
#include <string>

//  Receive 0MQ string from socket and convert into string
static std::string
receiveZmqMessage (zmq::socket_t & socket) {

    zmq::message_t message;
    socket.recv(&message);

    return std::string(static_cast<char*>(message.data()), message.size());
}

//  Convert string to 0MQ string and send to socket
static bool
sendZmqMessage (zmq::socket_t & socket, const std::string & string) {

    zmq::message_t message(string.size());
    memcpy (message.data(), string.data(), string.size());

    bool rc = socket.send (message);
    return (rc);
}

//  Sends string as 0MQ string, as multipart non-terminal
static bool
sendMultipartZmqMessage (zmq::socket_t & socket, const std::string & string) {

    zmq::message_t message(string.size());
    memcpy (message.data(), string.data(), string.size());

    bool rc = socket.send (message, ZMQ_SNDMORE);
    return (rc);
}

#endif
