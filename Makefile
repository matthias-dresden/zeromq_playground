CXXFLAGS =	-O3 -g -Wall -fmessage-length=0

EXE_SRC = zmqdataserver.cpp \
          zmqdataclient.cpp 

OBJS =	$(EXE_SRC:.cpp=.o)

LDFLAGS = -lprotobuf -lzmq 


PROTODEF = processdata.proto
PROTOHEADER = $(PROTODEF:.proto=.pb.h)
PROTOSOURCE = $(PROTODEF:.proto=.pb.cc)
PROTOFILES = $(PROTOHEADER) $(PROTOSOURCE)

BUILDDIR = build

EXECUTABLES = $(addprefix $(BUILDDIR)/, $(EXE_SRC:.cpp=))

$(PROTOFILES):
	protoc --cpp_out=. $^

$(EXE_SRC): $(PROTOFILES)

$(BUILDDIR)/%:	%.o $(PROTOSOURCE)
	mkdir -p $(BUILDDIR)
	$(CXX) -o $@ $^ $(LIBS) $(LDFLAGS)

all: $(EXECUTABLES)

clean:
	rm -rf $(OBJS) $(BUILDDIR)
