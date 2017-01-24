CXXFLAGS =	-O3 -g -Wall -fmessage-length=0

EXE_SRC = zmqdataserver.cpp \
          zmqdataclient.cpp 

OBJS =	$(EXE_SRC:.cpp=.o)

LDFLAGS = -lprotobuf -lzmq 


PROTODEF = common/processdata.proto
PROTOHEADER = cpp/$(PROTODEF:.proto=.pb.h)
PROTOSOURCE = cpp/$(PROTODEF:.proto=.pb.cc)
PROTOFILES = $(PROTOHEADER) $(PROTOSOURCE)

BUILDDIR = buildcpp

EXECUTABLES = $(addprefix $(BUILDDIR)/, $(EXE_SRC:.cpp=))

$(PROTOFILES):
	protoc --cpp_out=cpp/ $^

$(EXE_SRC): $(PROTOFILES)

$(BUILDDIR)/%:	cpp/%.o $(PROTOSOURCE)
	mkdir -p $(BUILDDIR)
	$(CXX) -o $@ $^ $(LIBS) $(LDFLAGS)

all: $(EXECUTABLES)

clean:
	rm -rf $(OBJS) $(BUILDDIR)
