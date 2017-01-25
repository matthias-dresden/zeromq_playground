CXXFLAGS =	-O3 -g -Wall -fmessage-length=0

#TOP_DIR = ../..

CC_SRC_DIR = src/cpp
PROTO_SRC_DIR = src/proto

BUILD_DIR = build/buildcpp

EXE_SRC = zmqdataserver.cpp \
          zmqdataclient.cpp 

OBJS =	$(EXE_SRC:.cpp=.o)

LDLIBS = -lprotobuf -lzmq 
CXXFLAGS += -I$(PROTO_SRC_DIR) -I./

PROTODEF = processdata.proto
PROTOHEADER = $(PROTO_SRC_DIR)/$(PROTODEF:.proto=.pb.h)
PROTOSOURCE = $(PROTO_SRC_DIR)/$(PROTODEF:.proto=.pb.cc)
PROTOOBJ=$(PROTOSOURCE:.cc=.o)

PROTOFILES = $(PROTOHEADER) $(PROTOSOURCE)

EXECUTABLES = $(addprefix $(BUILD_DIR)/, $(EXE_SRC:.cpp=))

$(PROTOFILES): $(PROTO_SRC_DIR)/$(PROTODEF)
	protoc --cpp_out=. $^

$(EXE_SRC): $(PROTOFILES)

$(BUILD_DIR)/%:	$(CC_SRC_DIR)/%.o $(PROTOOBJ)
	mkdir -p $(BUILD_DIR)
	$(CXX) -o $@ $^ $(LIBS) $(LDLIBS)

all: $(EXECUTABLES)

clean:
	rm -rf $(OBJS) $(BUILDDIR) $(PROTOFILES)
