// Code generated by protoc-gen-go. DO NOT EDIT.
// versions:
// 	protoc-gen-go v1.27.1
// 	protoc        v3.19.1
// source: matchmaker_allocation.proto

package allocation

import (
	protoreflect "google.golang.org/protobuf/reflect/protoreflect"
	protoimpl "google.golang.org/protobuf/runtime/protoimpl"
	emptypb "google.golang.org/protobuf/types/known/emptypb"
	reflect "reflect"
	sync "sync"
)

const (
	// Verify that this generated code is sufficiently up-to-date.
	_ = protoimpl.EnforceVersion(20 - protoimpl.MinVersion)
	// Verify that runtime/protoimpl is sufficiently up-to-date.
	_ = protoimpl.EnforceVersion(protoimpl.MaxVersion - 20)
)

type RequestType int32

const (
	RequestType_REQUEST_TYPE_UNSPECIFIED RequestType = 0
	RequestType_SERVER_CONNECT           RequestType = 1
	RequestType_SERVER_RECONNECT         RequestType = 2
	RequestType_SINGLE_MATCH_REQUEST     RequestType = 3
	RequestType_GROUP_MATCH_REQUEST      RequestType = 4
)

// Enum value maps for RequestType.
var (
	RequestType_name = map[int32]string{
		0: "REQUEST_TYPE_UNSPECIFIED",
		1: "SERVER_CONNECT",
		2: "SERVER_RECONNECT",
		3: "SINGLE_MATCH_REQUEST",
		4: "GROUP_MATCH_REQUEST",
	}
	RequestType_value = map[string]int32{
		"REQUEST_TYPE_UNSPECIFIED": 0,
		"SERVER_CONNECT":           1,
		"SERVER_RECONNECT":         2,
		"SINGLE_MATCH_REQUEST":     3,
		"GROUP_MATCH_REQUEST":      4,
	}
)

func (x RequestType) Enum() *RequestType {
	p := new(RequestType)
	*p = x
	return p
}

func (x RequestType) String() string {
	return protoimpl.X.EnumStringOf(x.Descriptor(), protoreflect.EnumNumber(x))
}

func (RequestType) Descriptor() protoreflect.EnumDescriptor {
	return file_matchmaker_allocation_proto_enumTypes[0].Descriptor()
}

func (RequestType) Type() protoreflect.EnumType {
	return &file_matchmaker_allocation_proto_enumTypes[0]
}

func (x RequestType) Number() protoreflect.EnumNumber {
	return protoreflect.EnumNumber(x)
}

// Deprecated: Use RequestType.Descriptor instead.
func (RequestType) EnumDescriptor() ([]byte, []int) {
	return file_matchmaker_allocation_proto_rawDescGZIP(), []int{0}
}

type MatchMakerAllocationRequest struct {
	state         protoimpl.MessageState
	sizeCache     protoimpl.SizeCache
	unknownFields protoimpl.UnknownFields

	Type    RequestType `protobuf:"varint,1,opt,name=type,proto3,enum=matchmaker.allocation.RequestType" json:"type,omitempty"`
	FleetId string      `protobuf:"bytes,2,opt,name=fleet_id,json=fleetId,proto3" json:"fleet_id,omitempty"`
}

func (x *MatchMakerAllocationRequest) Reset() {
	*x = MatchMakerAllocationRequest{}
	if protoimpl.UnsafeEnabled {
		mi := &file_matchmaker_allocation_proto_msgTypes[0]
		ms := protoimpl.X.MessageStateOf(protoimpl.Pointer(x))
		ms.StoreMessageInfo(mi)
	}
}

func (x *MatchMakerAllocationRequest) String() string {
	return protoimpl.X.MessageStringOf(x)
}

func (*MatchMakerAllocationRequest) ProtoMessage() {}

func (x *MatchMakerAllocationRequest) ProtoReflect() protoreflect.Message {
	mi := &file_matchmaker_allocation_proto_msgTypes[0]
	if protoimpl.UnsafeEnabled && x != nil {
		ms := protoimpl.X.MessageStateOf(protoimpl.Pointer(x))
		if ms.LoadMessageInfo() == nil {
			ms.StoreMessageInfo(mi)
		}
		return ms
	}
	return mi.MessageOf(x)
}

// Deprecated: Use MatchMakerAllocationRequest.ProtoReflect.Descriptor instead.
func (*MatchMakerAllocationRequest) Descriptor() ([]byte, []int) {
	return file_matchmaker_allocation_proto_rawDescGZIP(), []int{0}
}

func (x *MatchMakerAllocationRequest) GetType() RequestType {
	if x != nil {
		return x.Type
	}
	return RequestType_REQUEST_TYPE_UNSPECIFIED
}

func (x *MatchMakerAllocationRequest) GetFleetId() string {
	if x != nil {
		return x.FleetId
	}
	return ""
}

type MatchMakerAllocationResponse struct {
	state         protoimpl.MessageState
	sizeCache     protoimpl.SizeCache
	unknownFields protoimpl.UnknownFields

	GameServerName string                               `protobuf:"bytes,1,opt,name=game_server_name,json=gameServerName,proto3" json:"game_server_name,omitempty"`
	Address        string                               `protobuf:"bytes,2,opt,name=address,proto3" json:"address,omitempty"`
	NodeName       string                               `protobuf:"bytes,3,opt,name=node_name,json=nodeName,proto3" json:"node_name,omitempty"`
	Ports          []*MatchMakerAllocationResponse_Port `protobuf:"bytes,4,rep,name=ports,proto3" json:"ports,omitempty"`
}

func (x *MatchMakerAllocationResponse) Reset() {
	*x = MatchMakerAllocationResponse{}
	if protoimpl.UnsafeEnabled {
		mi := &file_matchmaker_allocation_proto_msgTypes[1]
		ms := protoimpl.X.MessageStateOf(protoimpl.Pointer(x))
		ms.StoreMessageInfo(mi)
	}
}

func (x *MatchMakerAllocationResponse) String() string {
	return protoimpl.X.MessageStringOf(x)
}

func (*MatchMakerAllocationResponse) ProtoMessage() {}

func (x *MatchMakerAllocationResponse) ProtoReflect() protoreflect.Message {
	mi := &file_matchmaker_allocation_proto_msgTypes[1]
	if protoimpl.UnsafeEnabled && x != nil {
		ms := protoimpl.X.MessageStateOf(protoimpl.Pointer(x))
		if ms.LoadMessageInfo() == nil {
			ms.StoreMessageInfo(mi)
		}
		return ms
	}
	return mi.MessageOf(x)
}

// Deprecated: Use MatchMakerAllocationResponse.ProtoReflect.Descriptor instead.
func (*MatchMakerAllocationResponse) Descriptor() ([]byte, []int) {
	return file_matchmaker_allocation_proto_rawDescGZIP(), []int{1}
}

func (x *MatchMakerAllocationResponse) GetGameServerName() string {
	if x != nil {
		return x.GameServerName
	}
	return ""
}

func (x *MatchMakerAllocationResponse) GetAddress() string {
	if x != nil {
		return x.Address
	}
	return ""
}

func (x *MatchMakerAllocationResponse) GetNodeName() string {
	if x != nil {
		return x.NodeName
	}
	return ""
}

func (x *MatchMakerAllocationResponse) GetPorts() []*MatchMakerAllocationResponse_Port {
	if x != nil {
		return x.Ports
	}
	return nil
}

type AllocatedGameServersResponse struct {
	state         protoimpl.MessageState
	sizeCache     protoimpl.SizeCache
	unknownFields protoimpl.UnknownFields

	AllocatedGameServers []string `protobuf:"bytes,1,rep,name=allocated_game_servers,json=allocatedGameServers,proto3" json:"allocated_game_servers,omitempty"`
}

func (x *AllocatedGameServersResponse) Reset() {
	*x = AllocatedGameServersResponse{}
	if protoimpl.UnsafeEnabled {
		mi := &file_matchmaker_allocation_proto_msgTypes[2]
		ms := protoimpl.X.MessageStateOf(protoimpl.Pointer(x))
		ms.StoreMessageInfo(mi)
	}
}

func (x *AllocatedGameServersResponse) String() string {
	return protoimpl.X.MessageStringOf(x)
}

func (*AllocatedGameServersResponse) ProtoMessage() {}

func (x *AllocatedGameServersResponse) ProtoReflect() protoreflect.Message {
	mi := &file_matchmaker_allocation_proto_msgTypes[2]
	if protoimpl.UnsafeEnabled && x != nil {
		ms := protoimpl.X.MessageStateOf(protoimpl.Pointer(x))
		if ms.LoadMessageInfo() == nil {
			ms.StoreMessageInfo(mi)
		}
		return ms
	}
	return mi.MessageOf(x)
}

// Deprecated: Use AllocatedGameServersResponse.ProtoReflect.Descriptor instead.
func (*AllocatedGameServersResponse) Descriptor() ([]byte, []int) {
	return file_matchmaker_allocation_proto_rawDescGZIP(), []int{2}
}

func (x *AllocatedGameServersResponse) GetAllocatedGameServers() []string {
	if x != nil {
		return x.AllocatedGameServers
	}
	return nil
}

type AllocatedGameServersRequest struct {
	state         protoimpl.MessageState
	sizeCache     protoimpl.SizeCache
	unknownFields protoimpl.UnknownFields

	AllocatedGameServers []string `protobuf:"bytes,1,rep,name=allocated_game_servers,json=allocatedGameServers,proto3" json:"allocated_game_servers,omitempty"`
}

func (x *AllocatedGameServersRequest) Reset() {
	*x = AllocatedGameServersRequest{}
	if protoimpl.UnsafeEnabled {
		mi := &file_matchmaker_allocation_proto_msgTypes[3]
		ms := protoimpl.X.MessageStateOf(protoimpl.Pointer(x))
		ms.StoreMessageInfo(mi)
	}
}

func (x *AllocatedGameServersRequest) String() string {
	return protoimpl.X.MessageStringOf(x)
}

func (*AllocatedGameServersRequest) ProtoMessage() {}

func (x *AllocatedGameServersRequest) ProtoReflect() protoreflect.Message {
	mi := &file_matchmaker_allocation_proto_msgTypes[3]
	if protoimpl.UnsafeEnabled && x != nil {
		ms := protoimpl.X.MessageStateOf(protoimpl.Pointer(x))
		if ms.LoadMessageInfo() == nil {
			ms.StoreMessageInfo(mi)
		}
		return ms
	}
	return mi.MessageOf(x)
}

// Deprecated: Use AllocatedGameServersRequest.ProtoReflect.Descriptor instead.
func (*AllocatedGameServersRequest) Descriptor() ([]byte, []int) {
	return file_matchmaker_allocation_proto_rawDescGZIP(), []int{3}
}

func (x *AllocatedGameServersRequest) GetAllocatedGameServers() []string {
	if x != nil {
		return x.AllocatedGameServers
	}
	return nil
}

type MatchMakerAllocationResponse_Port struct {
	state         protoimpl.MessageState
	sizeCache     protoimpl.SizeCache
	unknownFields protoimpl.UnknownFields

	Name string `protobuf:"bytes,1,opt,name=name,proto3" json:"name,omitempty"`
	Port int32  `protobuf:"varint,2,opt,name=port,proto3" json:"port,omitempty"`
}

func (x *MatchMakerAllocationResponse_Port) Reset() {
	*x = MatchMakerAllocationResponse_Port{}
	if protoimpl.UnsafeEnabled {
		mi := &file_matchmaker_allocation_proto_msgTypes[4]
		ms := protoimpl.X.MessageStateOf(protoimpl.Pointer(x))
		ms.StoreMessageInfo(mi)
	}
}

func (x *MatchMakerAllocationResponse_Port) String() string {
	return protoimpl.X.MessageStringOf(x)
}

func (*MatchMakerAllocationResponse_Port) ProtoMessage() {}

func (x *MatchMakerAllocationResponse_Port) ProtoReflect() protoreflect.Message {
	mi := &file_matchmaker_allocation_proto_msgTypes[4]
	if protoimpl.UnsafeEnabled && x != nil {
		ms := protoimpl.X.MessageStateOf(protoimpl.Pointer(x))
		if ms.LoadMessageInfo() == nil {
			ms.StoreMessageInfo(mi)
		}
		return ms
	}
	return mi.MessageOf(x)
}

// Deprecated: Use MatchMakerAllocationResponse_Port.ProtoReflect.Descriptor instead.
func (*MatchMakerAllocationResponse_Port) Descriptor() ([]byte, []int) {
	return file_matchmaker_allocation_proto_rawDescGZIP(), []int{1, 0}
}

func (x *MatchMakerAllocationResponse_Port) GetName() string {
	if x != nil {
		return x.Name
	}
	return ""
}

func (x *MatchMakerAllocationResponse_Port) GetPort() int32 {
	if x != nil {
		return x.Port
	}
	return 0
}

var File_matchmaker_allocation_proto protoreflect.FileDescriptor

var file_matchmaker_allocation_proto_rawDesc = []byte{
	0x0a, 0x1b, 0x6d, 0x61, 0x74, 0x63, 0x68, 0x6d, 0x61, 0x6b, 0x65, 0x72, 0x5f, 0x61, 0x6c, 0x6c,
	0x6f, 0x63, 0x61, 0x74, 0x69, 0x6f, 0x6e, 0x2e, 0x70, 0x72, 0x6f, 0x74, 0x6f, 0x12, 0x15, 0x6d,
	0x61, 0x74, 0x63, 0x68, 0x6d, 0x61, 0x6b, 0x65, 0x72, 0x2e, 0x61, 0x6c, 0x6c, 0x6f, 0x63, 0x61,
	0x74, 0x69, 0x6f, 0x6e, 0x1a, 0x1b, 0x67, 0x6f, 0x6f, 0x67, 0x6c, 0x65, 0x2f, 0x70, 0x72, 0x6f,
	0x74, 0x6f, 0x62, 0x75, 0x66, 0x2f, 0x65, 0x6d, 0x70, 0x74, 0x79, 0x2e, 0x70, 0x72, 0x6f, 0x74,
	0x6f, 0x22, 0x70, 0x0a, 0x1b, 0x4d, 0x61, 0x74, 0x63, 0x68, 0x4d, 0x61, 0x6b, 0x65, 0x72, 0x41,
	0x6c, 0x6c, 0x6f, 0x63, 0x61, 0x74, 0x69, 0x6f, 0x6e, 0x52, 0x65, 0x71, 0x75, 0x65, 0x73, 0x74,
	0x12, 0x36, 0x0a, 0x04, 0x74, 0x79, 0x70, 0x65, 0x18, 0x01, 0x20, 0x01, 0x28, 0x0e, 0x32, 0x22,
	0x2e, 0x6d, 0x61, 0x74, 0x63, 0x68, 0x6d, 0x61, 0x6b, 0x65, 0x72, 0x2e, 0x61, 0x6c, 0x6c, 0x6f,
	0x63, 0x61, 0x74, 0x69, 0x6f, 0x6e, 0x2e, 0x52, 0x65, 0x71, 0x75, 0x65, 0x73, 0x74, 0x54, 0x79,
	0x70, 0x65, 0x52, 0x04, 0x74, 0x79, 0x70, 0x65, 0x12, 0x19, 0x0a, 0x08, 0x66, 0x6c, 0x65, 0x65,
	0x74, 0x5f, 0x69, 0x64, 0x18, 0x02, 0x20, 0x01, 0x28, 0x09, 0x52, 0x07, 0x66, 0x6c, 0x65, 0x65,
	0x74, 0x49, 0x64, 0x22, 0xff, 0x01, 0x0a, 0x1c, 0x4d, 0x61, 0x74, 0x63, 0x68, 0x4d, 0x61, 0x6b,
	0x65, 0x72, 0x41, 0x6c, 0x6c, 0x6f, 0x63, 0x61, 0x74, 0x69, 0x6f, 0x6e, 0x52, 0x65, 0x73, 0x70,
	0x6f, 0x6e, 0x73, 0x65, 0x12, 0x28, 0x0a, 0x10, 0x67, 0x61, 0x6d, 0x65, 0x5f, 0x73, 0x65, 0x72,
	0x76, 0x65, 0x72, 0x5f, 0x6e, 0x61, 0x6d, 0x65, 0x18, 0x01, 0x20, 0x01, 0x28, 0x09, 0x52, 0x0e,
	0x67, 0x61, 0x6d, 0x65, 0x53, 0x65, 0x72, 0x76, 0x65, 0x72, 0x4e, 0x61, 0x6d, 0x65, 0x12, 0x18,
	0x0a, 0x07, 0x61, 0x64, 0x64, 0x72, 0x65, 0x73, 0x73, 0x18, 0x02, 0x20, 0x01, 0x28, 0x09, 0x52,
	0x07, 0x61, 0x64, 0x64, 0x72, 0x65, 0x73, 0x73, 0x12, 0x1b, 0x0a, 0x09, 0x6e, 0x6f, 0x64, 0x65,
	0x5f, 0x6e, 0x61, 0x6d, 0x65, 0x18, 0x03, 0x20, 0x01, 0x28, 0x09, 0x52, 0x08, 0x6e, 0x6f, 0x64,
	0x65, 0x4e, 0x61, 0x6d, 0x65, 0x12, 0x4e, 0x0a, 0x05, 0x70, 0x6f, 0x72, 0x74, 0x73, 0x18, 0x04,
	0x20, 0x03, 0x28, 0x0b, 0x32, 0x38, 0x2e, 0x6d, 0x61, 0x74, 0x63, 0x68, 0x6d, 0x61, 0x6b, 0x65,
	0x72, 0x2e, 0x61, 0x6c, 0x6c, 0x6f, 0x63, 0x61, 0x74, 0x69, 0x6f, 0x6e, 0x2e, 0x4d, 0x61, 0x74,
	0x63, 0x68, 0x4d, 0x61, 0x6b, 0x65, 0x72, 0x41, 0x6c, 0x6c, 0x6f, 0x63, 0x61, 0x74, 0x69, 0x6f,
	0x6e, 0x52, 0x65, 0x73, 0x70, 0x6f, 0x6e, 0x73, 0x65, 0x2e, 0x50, 0x6f, 0x72, 0x74, 0x52, 0x05,
	0x70, 0x6f, 0x72, 0x74, 0x73, 0x1a, 0x2e, 0x0a, 0x04, 0x50, 0x6f, 0x72, 0x74, 0x12, 0x12, 0x0a,
	0x04, 0x6e, 0x61, 0x6d, 0x65, 0x18, 0x01, 0x20, 0x01, 0x28, 0x09, 0x52, 0x04, 0x6e, 0x61, 0x6d,
	0x65, 0x12, 0x12, 0x0a, 0x04, 0x70, 0x6f, 0x72, 0x74, 0x18, 0x02, 0x20, 0x01, 0x28, 0x05, 0x52,
	0x04, 0x70, 0x6f, 0x72, 0x74, 0x22, 0x54, 0x0a, 0x1c, 0x41, 0x6c, 0x6c, 0x6f, 0x63, 0x61, 0x74,
	0x65, 0x64, 0x47, 0x61, 0x6d, 0x65, 0x53, 0x65, 0x72, 0x76, 0x65, 0x72, 0x73, 0x52, 0x65, 0x73,
	0x70, 0x6f, 0x6e, 0x73, 0x65, 0x12, 0x34, 0x0a, 0x16, 0x61, 0x6c, 0x6c, 0x6f, 0x63, 0x61, 0x74,
	0x65, 0x64, 0x5f, 0x67, 0x61, 0x6d, 0x65, 0x5f, 0x73, 0x65, 0x72, 0x76, 0x65, 0x72, 0x73, 0x18,
	0x01, 0x20, 0x03, 0x28, 0x09, 0x52, 0x14, 0x61, 0x6c, 0x6c, 0x6f, 0x63, 0x61, 0x74, 0x65, 0x64,
	0x47, 0x61, 0x6d, 0x65, 0x53, 0x65, 0x72, 0x76, 0x65, 0x72, 0x73, 0x22, 0x53, 0x0a, 0x1b, 0x41,
	0x6c, 0x6c, 0x6f, 0x63, 0x61, 0x74, 0x65, 0x64, 0x47, 0x61, 0x6d, 0x65, 0x53, 0x65, 0x72, 0x76,
	0x65, 0x72, 0x73, 0x52, 0x65, 0x71, 0x75, 0x65, 0x73, 0x74, 0x12, 0x34, 0x0a, 0x16, 0x61, 0x6c,
	0x6c, 0x6f, 0x63, 0x61, 0x74, 0x65, 0x64, 0x5f, 0x67, 0x61, 0x6d, 0x65, 0x5f, 0x73, 0x65, 0x72,
	0x76, 0x65, 0x72, 0x73, 0x18, 0x01, 0x20, 0x03, 0x28, 0x09, 0x52, 0x14, 0x61, 0x6c, 0x6c, 0x6f,
	0x63, 0x61, 0x74, 0x65, 0x64, 0x47, 0x61, 0x6d, 0x65, 0x53, 0x65, 0x72, 0x76, 0x65, 0x72, 0x73,
	0x2a, 0x88, 0x01, 0x0a, 0x0b, 0x52, 0x65, 0x71, 0x75, 0x65, 0x73, 0x74, 0x54, 0x79, 0x70, 0x65,
	0x12, 0x1c, 0x0a, 0x18, 0x52, 0x45, 0x51, 0x55, 0x45, 0x53, 0x54, 0x5f, 0x54, 0x59, 0x50, 0x45,
	0x5f, 0x55, 0x4e, 0x53, 0x50, 0x45, 0x43, 0x49, 0x46, 0x49, 0x45, 0x44, 0x10, 0x00, 0x12, 0x12,
	0x0a, 0x0e, 0x53, 0x45, 0x52, 0x56, 0x45, 0x52, 0x5f, 0x43, 0x4f, 0x4e, 0x4e, 0x45, 0x43, 0x54,
	0x10, 0x01, 0x12, 0x14, 0x0a, 0x10, 0x53, 0x45, 0x52, 0x56, 0x45, 0x52, 0x5f, 0x52, 0x45, 0x43,
	0x4f, 0x4e, 0x4e, 0x45, 0x43, 0x54, 0x10, 0x02, 0x12, 0x18, 0x0a, 0x14, 0x53, 0x49, 0x4e, 0x47,
	0x4c, 0x45, 0x5f, 0x4d, 0x41, 0x54, 0x43, 0x48, 0x5f, 0x52, 0x45, 0x51, 0x55, 0x45, 0x53, 0x54,
	0x10, 0x03, 0x12, 0x17, 0x0a, 0x13, 0x47, 0x52, 0x4f, 0x55, 0x50, 0x5f, 0x4d, 0x41, 0x54, 0x43,
	0x48, 0x5f, 0x52, 0x45, 0x51, 0x55, 0x45, 0x53, 0x54, 0x10, 0x04, 0x32, 0xef, 0x02, 0x0a, 0x1b,
	0x4d, 0x61, 0x74, 0x63, 0x68, 0x4d, 0x61, 0x6b, 0x65, 0x72, 0x41, 0x6c, 0x6c, 0x6f, 0x63, 0x61,
	0x74, 0x69, 0x6f, 0x6e, 0x53, 0x65, 0x72, 0x76, 0x69, 0x63, 0x65, 0x12, 0x7a, 0x0a, 0x0d, 0x47,
	0x65, 0x74, 0x41, 0x6c, 0x6c, 0x6f, 0x63, 0x61, 0x74, 0x69, 0x6f, 0x6e, 0x12, 0x32, 0x2e, 0x6d,
	0x61, 0x74, 0x63, 0x68, 0x6d, 0x61, 0x6b, 0x65, 0x72, 0x2e, 0x61, 0x6c, 0x6c, 0x6f, 0x63, 0x61,
	0x74, 0x69, 0x6f, 0x6e, 0x2e, 0x4d, 0x61, 0x74, 0x63, 0x68, 0x4d, 0x61, 0x6b, 0x65, 0x72, 0x41,
	0x6c, 0x6c, 0x6f, 0x63, 0x61, 0x74, 0x69, 0x6f, 0x6e, 0x52, 0x65, 0x71, 0x75, 0x65, 0x73, 0x74,
	0x1a, 0x33, 0x2e, 0x6d, 0x61, 0x74, 0x63, 0x68, 0x6d, 0x61, 0x6b, 0x65, 0x72, 0x2e, 0x61, 0x6c,
	0x6c, 0x6f, 0x63, 0x61, 0x74, 0x69, 0x6f, 0x6e, 0x2e, 0x4d, 0x61, 0x74, 0x63, 0x68, 0x4d, 0x61,
	0x6b, 0x65, 0x72, 0x41, 0x6c, 0x6c, 0x6f, 0x63, 0x61, 0x74, 0x69, 0x6f, 0x6e, 0x52, 0x65, 0x73,
	0x70, 0x6f, 0x6e, 0x73, 0x65, 0x22, 0x00, 0x12, 0x68, 0x0a, 0x17, 0x47, 0x65, 0x74, 0x41, 0x6c,
	0x6c, 0x6f, 0x63, 0x61, 0x74, 0x65, 0x64, 0x47, 0x61, 0x6d, 0x65, 0x53, 0x65, 0x72, 0x76, 0x65,
	0x72, 0x73, 0x12, 0x16, 0x2e, 0x67, 0x6f, 0x6f, 0x67, 0x6c, 0x65, 0x2e, 0x70, 0x72, 0x6f, 0x74,
	0x6f, 0x62, 0x75, 0x66, 0x2e, 0x45, 0x6d, 0x70, 0x74, 0x79, 0x1a, 0x33, 0x2e, 0x6d, 0x61, 0x74,
	0x63, 0x68, 0x6d, 0x61, 0x6b, 0x65, 0x72, 0x2e, 0x61, 0x6c, 0x6c, 0x6f, 0x63, 0x61, 0x74, 0x69,
	0x6f, 0x6e, 0x2e, 0x41, 0x6c, 0x6c, 0x6f, 0x63, 0x61, 0x74, 0x65, 0x64, 0x47, 0x61, 0x6d, 0x65,
	0x53, 0x65, 0x72, 0x76, 0x65, 0x72, 0x73, 0x52, 0x65, 0x73, 0x70, 0x6f, 0x6e, 0x73, 0x65, 0x22,
	0x00, 0x12, 0x6a, 0x0a, 0x1a, 0x44, 0x65, 0x6c, 0x65, 0x74, 0x65, 0x41, 0x6c, 0x6c, 0x6f, 0x63,
	0x61, 0x74, 0x65, 0x64, 0x47, 0x61, 0x6d, 0x65, 0x53, 0x65, 0x72, 0x76, 0x65, 0x72, 0x73, 0x12,
	0x32, 0x2e, 0x6d, 0x61, 0x74, 0x63, 0x68, 0x6d, 0x61, 0x6b, 0x65, 0x72, 0x2e, 0x61, 0x6c, 0x6c,
	0x6f, 0x63, 0x61, 0x74, 0x69, 0x6f, 0x6e, 0x2e, 0x41, 0x6c, 0x6c, 0x6f, 0x63, 0x61, 0x74, 0x65,
	0x64, 0x47, 0x61, 0x6d, 0x65, 0x53, 0x65, 0x72, 0x76, 0x65, 0x72, 0x73, 0x52, 0x65, 0x71, 0x75,
	0x65, 0x73, 0x74, 0x1a, 0x16, 0x2e, 0x67, 0x6f, 0x6f, 0x67, 0x6c, 0x65, 0x2e, 0x70, 0x72, 0x6f,
	0x74, 0x6f, 0x62, 0x75, 0x66, 0x2e, 0x45, 0x6d, 0x70, 0x74, 0x79, 0x22, 0x00, 0x42, 0xa1, 0x01,
	0x0a, 0x2c, 0x69, 0x6f, 0x2e, 0x6d, 0x75, 0x7a, 0x69, 0x76, 0x65, 0x72, 0x73, 0x65, 0x2e, 0x70,
	0x72, 0x6f, 0x74, 0x6f, 0x2e, 0x6d, 0x61, 0x74, 0x63, 0x68, 0x6d, 0x61, 0x6b, 0x65, 0x72, 0x2e,
	0x61, 0x70, 0x69, 0x2e, 0x61, 0x6c, 0x6c, 0x6f, 0x63, 0x61, 0x74, 0x69, 0x6f, 0x6e, 0x42, 0x19,
	0x4d, 0x61, 0x74, 0x63, 0x68, 0x4d, 0x61, 0x6b, 0x65, 0x72, 0x41, 0x6c, 0x6c, 0x6f, 0x63, 0x61,
	0x74, 0x69, 0x6f, 0x6e, 0x50, 0x72, 0x6f, 0x74, 0x6f, 0x50, 0x01, 0x5a, 0x54, 0x67, 0x69, 0x74,
	0x6c, 0x61, 0x62, 0x2e, 0x63, 0x6f, 0x6d, 0x2f, 0x6d, 0x75, 0x7a, 0x69, 0x76, 0x65, 0x72, 0x73,
	0x65, 0x2f, 0x63, 0x6f, 0x6d, 0x6d, 0x6f, 0x6e, 0x2f, 0x70, 0x72, 0x6f, 0x74, 0x6f, 0x67, 0x6f,
	0x67, 0x65, 0x6e, 0x65, 0x72, 0x61, 0x74, 0x6f, 0x72, 0x2f, 0x6d, 0x61, 0x74, 0x63, 0x68, 0x6d,
	0x61, 0x6b, 0x65, 0x72, 0x2d, 0x70, 0x72, 0x6f, 0x74, 0x6f, 0x2d, 0x61, 0x70, 0x69, 0x2f, 0x70,
	0x6b, 0x67, 0x2f, 0x61, 0x70, 0x69, 0x2f, 0x61, 0x6c, 0x6c, 0x6f, 0x63, 0x61, 0x74, 0x69, 0x6f,
	0x6e, 0x62, 0x06, 0x70, 0x72, 0x6f, 0x74, 0x6f, 0x33,
}

var (
	file_matchmaker_allocation_proto_rawDescOnce sync.Once
	file_matchmaker_allocation_proto_rawDescData = file_matchmaker_allocation_proto_rawDesc
)

func file_matchmaker_allocation_proto_rawDescGZIP() []byte {
	file_matchmaker_allocation_proto_rawDescOnce.Do(func() {
		file_matchmaker_allocation_proto_rawDescData = protoimpl.X.CompressGZIP(file_matchmaker_allocation_proto_rawDescData)
	})
	return file_matchmaker_allocation_proto_rawDescData
}

var file_matchmaker_allocation_proto_enumTypes = make([]protoimpl.EnumInfo, 1)
var file_matchmaker_allocation_proto_msgTypes = make([]protoimpl.MessageInfo, 5)
var file_matchmaker_allocation_proto_goTypes = []interface{}{
	(RequestType)(0),                          // 0: matchmaker.allocation.RequestType
	(*MatchMakerAllocationRequest)(nil),       // 1: matchmaker.allocation.MatchMakerAllocationRequest
	(*MatchMakerAllocationResponse)(nil),      // 2: matchmaker.allocation.MatchMakerAllocationResponse
	(*AllocatedGameServersResponse)(nil),      // 3: matchmaker.allocation.AllocatedGameServersResponse
	(*AllocatedGameServersRequest)(nil),       // 4: matchmaker.allocation.AllocatedGameServersRequest
	(*MatchMakerAllocationResponse_Port)(nil), // 5: matchmaker.allocation.MatchMakerAllocationResponse.Port
	(*emptypb.Empty)(nil),                     // 6: google.protobuf.Empty
}
var file_matchmaker_allocation_proto_depIdxs = []int32{
	0, // 0: matchmaker.allocation.MatchMakerAllocationRequest.type:type_name -> matchmaker.allocation.RequestType
	5, // 1: matchmaker.allocation.MatchMakerAllocationResponse.ports:type_name -> matchmaker.allocation.MatchMakerAllocationResponse.Port
	1, // 2: matchmaker.allocation.MatchMakerAllocationService.GetAllocation:input_type -> matchmaker.allocation.MatchMakerAllocationRequest
	6, // 3: matchmaker.allocation.MatchMakerAllocationService.GetAllocatedGameServers:input_type -> google.protobuf.Empty
	4, // 4: matchmaker.allocation.MatchMakerAllocationService.DeleteAllocatedGameServers:input_type -> matchmaker.allocation.AllocatedGameServersRequest
	2, // 5: matchmaker.allocation.MatchMakerAllocationService.GetAllocation:output_type -> matchmaker.allocation.MatchMakerAllocationResponse
	3, // 6: matchmaker.allocation.MatchMakerAllocationService.GetAllocatedGameServers:output_type -> matchmaker.allocation.AllocatedGameServersResponse
	6, // 7: matchmaker.allocation.MatchMakerAllocationService.DeleteAllocatedGameServers:output_type -> google.protobuf.Empty
	5, // [5:8] is the sub-list for method output_type
	2, // [2:5] is the sub-list for method input_type
	2, // [2:2] is the sub-list for extension type_name
	2, // [2:2] is the sub-list for extension extendee
	0, // [0:2] is the sub-list for field type_name
}

func init() { file_matchmaker_allocation_proto_init() }
func file_matchmaker_allocation_proto_init() {
	if File_matchmaker_allocation_proto != nil {
		return
	}
	if !protoimpl.UnsafeEnabled {
		file_matchmaker_allocation_proto_msgTypes[0].Exporter = func(v interface{}, i int) interface{} {
			switch v := v.(*MatchMakerAllocationRequest); i {
			case 0:
				return &v.state
			case 1:
				return &v.sizeCache
			case 2:
				return &v.unknownFields
			default:
				return nil
			}
		}
		file_matchmaker_allocation_proto_msgTypes[1].Exporter = func(v interface{}, i int) interface{} {
			switch v := v.(*MatchMakerAllocationResponse); i {
			case 0:
				return &v.state
			case 1:
				return &v.sizeCache
			case 2:
				return &v.unknownFields
			default:
				return nil
			}
		}
		file_matchmaker_allocation_proto_msgTypes[2].Exporter = func(v interface{}, i int) interface{} {
			switch v := v.(*AllocatedGameServersResponse); i {
			case 0:
				return &v.state
			case 1:
				return &v.sizeCache
			case 2:
				return &v.unknownFields
			default:
				return nil
			}
		}
		file_matchmaker_allocation_proto_msgTypes[3].Exporter = func(v interface{}, i int) interface{} {
			switch v := v.(*AllocatedGameServersRequest); i {
			case 0:
				return &v.state
			case 1:
				return &v.sizeCache
			case 2:
				return &v.unknownFields
			default:
				return nil
			}
		}
		file_matchmaker_allocation_proto_msgTypes[4].Exporter = func(v interface{}, i int) interface{} {
			switch v := v.(*MatchMakerAllocationResponse_Port); i {
			case 0:
				return &v.state
			case 1:
				return &v.sizeCache
			case 2:
				return &v.unknownFields
			default:
				return nil
			}
		}
	}
	type x struct{}
	out := protoimpl.TypeBuilder{
		File: protoimpl.DescBuilder{
			GoPackagePath: reflect.TypeOf(x{}).PkgPath(),
			RawDescriptor: file_matchmaker_allocation_proto_rawDesc,
			NumEnums:      1,
			NumMessages:   5,
			NumExtensions: 0,
			NumServices:   1,
		},
		GoTypes:           file_matchmaker_allocation_proto_goTypes,
		DependencyIndexes: file_matchmaker_allocation_proto_depIdxs,
		EnumInfos:         file_matchmaker_allocation_proto_enumTypes,
		MessageInfos:      file_matchmaker_allocation_proto_msgTypes,
	}.Build()
	File_matchmaker_allocation_proto = out.File
	file_matchmaker_allocation_proto_rawDesc = nil
	file_matchmaker_allocation_proto_goTypes = nil
	file_matchmaker_allocation_proto_depIdxs = nil
}
