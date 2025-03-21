// Code generated by protoc-gen-go. DO NOT EDIT.
// versions:
// 	protoc-gen-go v1.27.1
// 	protoc        v3.19.1
// source: domain/k8s_common.proto

package domain

import (
	protoreflect "google.golang.org/protobuf/reflect/protoreflect"
	protoimpl "google.golang.org/protobuf/runtime/protoimpl"
	reflect "reflect"
	sync "sync"
)

const (
	// Verify that this generated code is sufficiently up-to-date.
	_ = protoimpl.EnforceVersion(20 - protoimpl.MinVersion)
	// Verify that runtime/protoimpl is sufficiently up-to-date.
	_ = protoimpl.EnforceVersion(protoimpl.MaxVersion - 20)
)

type GameFleetType int32

const (
	GameFleetType_FLEET_TYPE_UNSPECIFIED GameFleetType = 0
	GameFleetType_ISLAND_INSTANCE        GameFleetType = 1
	GameFleetType_GAME_SERVER_INSTANCE   GameFleetType = 2
)

// Enum value maps for GameFleetType.
var (
	GameFleetType_name = map[int32]string{
		0: "FLEET_TYPE_UNSPECIFIED",
		1: "ISLAND_INSTANCE",
		2: "GAME_SERVER_INSTANCE",
	}
	GameFleetType_value = map[string]int32{
		"FLEET_TYPE_UNSPECIFIED": 0,
		"ISLAND_INSTANCE":        1,
		"GAME_SERVER_INSTANCE":   2,
	}
)

func (x GameFleetType) Enum() *GameFleetType {
	p := new(GameFleetType)
	*p = x
	return p
}

func (x GameFleetType) String() string {
	return protoimpl.X.EnumStringOf(x.Descriptor(), protoreflect.EnumNumber(x))
}

func (GameFleetType) Descriptor() protoreflect.EnumDescriptor {
	return file_domain_k8s_common_proto_enumTypes[0].Descriptor()
}

func (GameFleetType) Type() protoreflect.EnumType {
	return &file_domain_k8s_common_proto_enumTypes[0]
}

func (x GameFleetType) Number() protoreflect.EnumNumber {
	return protoreflect.EnumNumber(x)
}

// Deprecated: Use GameFleetType.Descriptor instead.
func (GameFleetType) EnumDescriptor() ([]byte, []int) {
	return file_domain_k8s_common_proto_rawDescGZIP(), []int{0}
}

type GameServer struct {
	state         protoimpl.MessageState
	sizeCache     protoimpl.SizeCache
	unknownFields protoimpl.UnknownFields

	GameServerId string             `protobuf:"bytes,1,opt,name=game_server_id,json=gameServerId,proto3" json:"game_server_id,omitempty"`
	Address      string             `protobuf:"bytes,2,opt,name=address,proto3" json:"address,omitempty"`
	NodeName     string             `protobuf:"bytes,3,opt,name=node_name,json=nodeName,proto3" json:"node_name,omitempty"`
	State        string             `protobuf:"bytes,4,opt,name=state,proto3" json:"state,omitempty"`
	Ports        []*GameServer_Port `protobuf:"bytes,5,rep,name=ports,proto3" json:"ports,omitempty"`
}

func (x *GameServer) Reset() {
	*x = GameServer{}
	if protoimpl.UnsafeEnabled {
		mi := &file_domain_k8s_common_proto_msgTypes[0]
		ms := protoimpl.X.MessageStateOf(protoimpl.Pointer(x))
		ms.StoreMessageInfo(mi)
	}
}

func (x *GameServer) String() string {
	return protoimpl.X.MessageStringOf(x)
}

func (*GameServer) ProtoMessage() {}

func (x *GameServer) ProtoReflect() protoreflect.Message {
	mi := &file_domain_k8s_common_proto_msgTypes[0]
	if protoimpl.UnsafeEnabled && x != nil {
		ms := protoimpl.X.MessageStateOf(protoimpl.Pointer(x))
		if ms.LoadMessageInfo() == nil {
			ms.StoreMessageInfo(mi)
		}
		return ms
	}
	return mi.MessageOf(x)
}

// Deprecated: Use GameServer.ProtoReflect.Descriptor instead.
func (*GameServer) Descriptor() ([]byte, []int) {
	return file_domain_k8s_common_proto_rawDescGZIP(), []int{0}
}

func (x *GameServer) GetGameServerId() string {
	if x != nil {
		return x.GameServerId
	}
	return ""
}

func (x *GameServer) GetAddress() string {
	if x != nil {
		return x.Address
	}
	return ""
}

func (x *GameServer) GetNodeName() string {
	if x != nil {
		return x.NodeName
	}
	return ""
}

func (x *GameServer) GetState() string {
	if x != nil {
		return x.State
	}
	return ""
}

func (x *GameServer) GetPorts() []*GameServer_Port {
	if x != nil {
		return x.Ports
	}
	return nil
}

type GameFleetResponse struct {
	state         protoimpl.MessageState
	sizeCache     protoimpl.SizeCache
	unknownFields protoimpl.UnknownFields

	FleetId             string        `protobuf:"bytes,1,opt,name=fleet_id,json=fleetId,proto3" json:"fleet_id,omitempty"`
	Namespace           string        `protobuf:"bytes,2,opt,name=namespace,proto3" json:"namespace,omitempty"`
	FleetType           GameFleetType `protobuf:"varint,3,opt,name=fleet_type,json=fleetType,proto3,enum=GameFleetType" json:"fleet_type,omitempty"`
	GameSymbol          string        `protobuf:"bytes,4,opt,name=game_symbol,json=gameSymbol,proto3" json:"game_symbol,omitempty"`
	NumberOfGameServers int64         `protobuf:"varint,5,opt,name=number_of_game_servers,json=numberOfGameServers,proto3" json:"number_of_game_servers,omitempty"`
	GameServers         []*GameServer `protobuf:"bytes,6,rep,name=game_servers,json=gameServers,proto3" json:"game_servers,omitempty"`
}

func (x *GameFleetResponse) Reset() {
	*x = GameFleetResponse{}
	if protoimpl.UnsafeEnabled {
		mi := &file_domain_k8s_common_proto_msgTypes[1]
		ms := protoimpl.X.MessageStateOf(protoimpl.Pointer(x))
		ms.StoreMessageInfo(mi)
	}
}

func (x *GameFleetResponse) String() string {
	return protoimpl.X.MessageStringOf(x)
}

func (*GameFleetResponse) ProtoMessage() {}

func (x *GameFleetResponse) ProtoReflect() protoreflect.Message {
	mi := &file_domain_k8s_common_proto_msgTypes[1]
	if protoimpl.UnsafeEnabled && x != nil {
		ms := protoimpl.X.MessageStateOf(protoimpl.Pointer(x))
		if ms.LoadMessageInfo() == nil {
			ms.StoreMessageInfo(mi)
		}
		return ms
	}
	return mi.MessageOf(x)
}

// Deprecated: Use GameFleetResponse.ProtoReflect.Descriptor instead.
func (*GameFleetResponse) Descriptor() ([]byte, []int) {
	return file_domain_k8s_common_proto_rawDescGZIP(), []int{1}
}

func (x *GameFleetResponse) GetFleetId() string {
	if x != nil {
		return x.FleetId
	}
	return ""
}

func (x *GameFleetResponse) GetNamespace() string {
	if x != nil {
		return x.Namespace
	}
	return ""
}

func (x *GameFleetResponse) GetFleetType() GameFleetType {
	if x != nil {
		return x.FleetType
	}
	return GameFleetType_FLEET_TYPE_UNSPECIFIED
}

func (x *GameFleetResponse) GetGameSymbol() string {
	if x != nil {
		return x.GameSymbol
	}
	return ""
}

func (x *GameFleetResponse) GetNumberOfGameServers() int64 {
	if x != nil {
		return x.NumberOfGameServers
	}
	return 0
}

func (x *GameFleetResponse) GetGameServers() []*GameServer {
	if x != nil {
		return x.GameServers
	}
	return nil
}

type GameServer_Port struct {
	state         protoimpl.MessageState
	sizeCache     protoimpl.SizeCache
	unknownFields protoimpl.UnknownFields

	Name string `protobuf:"bytes,1,opt,name=name,proto3" json:"name,omitempty"`
	Port int32  `protobuf:"varint,2,opt,name=port,proto3" json:"port,omitempty"`
}

func (x *GameServer_Port) Reset() {
	*x = GameServer_Port{}
	if protoimpl.UnsafeEnabled {
		mi := &file_domain_k8s_common_proto_msgTypes[2]
		ms := protoimpl.X.MessageStateOf(protoimpl.Pointer(x))
		ms.StoreMessageInfo(mi)
	}
}

func (x *GameServer_Port) String() string {
	return protoimpl.X.MessageStringOf(x)
}

func (*GameServer_Port) ProtoMessage() {}

func (x *GameServer_Port) ProtoReflect() protoreflect.Message {
	mi := &file_domain_k8s_common_proto_msgTypes[2]
	if protoimpl.UnsafeEnabled && x != nil {
		ms := protoimpl.X.MessageStateOf(protoimpl.Pointer(x))
		if ms.LoadMessageInfo() == nil {
			ms.StoreMessageInfo(mi)
		}
		return ms
	}
	return mi.MessageOf(x)
}

// Deprecated: Use GameServer_Port.ProtoReflect.Descriptor instead.
func (*GameServer_Port) Descriptor() ([]byte, []int) {
	return file_domain_k8s_common_proto_rawDescGZIP(), []int{0, 0}
}

func (x *GameServer_Port) GetName() string {
	if x != nil {
		return x.Name
	}
	return ""
}

func (x *GameServer_Port) GetPort() int32 {
	if x != nil {
		return x.Port
	}
	return 0
}

var File_domain_k8s_common_proto protoreflect.FileDescriptor

var file_domain_k8s_common_proto_rawDesc = []byte{
	0x0a, 0x17, 0x64, 0x6f, 0x6d, 0x61, 0x69, 0x6e, 0x2f, 0x6b, 0x38, 0x73, 0x5f, 0x63, 0x6f, 0x6d,
	0x6d, 0x6f, 0x6e, 0x2e, 0x70, 0x72, 0x6f, 0x74, 0x6f, 0x22, 0xd7, 0x01, 0x0a, 0x0a, 0x47, 0x61,
	0x6d, 0x65, 0x53, 0x65, 0x72, 0x76, 0x65, 0x72, 0x12, 0x24, 0x0a, 0x0e, 0x67, 0x61, 0x6d, 0x65,
	0x5f, 0x73, 0x65, 0x72, 0x76, 0x65, 0x72, 0x5f, 0x69, 0x64, 0x18, 0x01, 0x20, 0x01, 0x28, 0x09,
	0x52, 0x0c, 0x67, 0x61, 0x6d, 0x65, 0x53, 0x65, 0x72, 0x76, 0x65, 0x72, 0x49, 0x64, 0x12, 0x18,
	0x0a, 0x07, 0x61, 0x64, 0x64, 0x72, 0x65, 0x73, 0x73, 0x18, 0x02, 0x20, 0x01, 0x28, 0x09, 0x52,
	0x07, 0x61, 0x64, 0x64, 0x72, 0x65, 0x73, 0x73, 0x12, 0x1b, 0x0a, 0x09, 0x6e, 0x6f, 0x64, 0x65,
	0x5f, 0x6e, 0x61, 0x6d, 0x65, 0x18, 0x03, 0x20, 0x01, 0x28, 0x09, 0x52, 0x08, 0x6e, 0x6f, 0x64,
	0x65, 0x4e, 0x61, 0x6d, 0x65, 0x12, 0x14, 0x0a, 0x05, 0x73, 0x74, 0x61, 0x74, 0x65, 0x18, 0x04,
	0x20, 0x01, 0x28, 0x09, 0x52, 0x05, 0x73, 0x74, 0x61, 0x74, 0x65, 0x12, 0x26, 0x0a, 0x05, 0x70,
	0x6f, 0x72, 0x74, 0x73, 0x18, 0x05, 0x20, 0x03, 0x28, 0x0b, 0x32, 0x10, 0x2e, 0x47, 0x61, 0x6d,
	0x65, 0x53, 0x65, 0x72, 0x76, 0x65, 0x72, 0x2e, 0x50, 0x6f, 0x72, 0x74, 0x52, 0x05, 0x70, 0x6f,
	0x72, 0x74, 0x73, 0x1a, 0x2e, 0x0a, 0x04, 0x50, 0x6f, 0x72, 0x74, 0x12, 0x12, 0x0a, 0x04, 0x6e,
	0x61, 0x6d, 0x65, 0x18, 0x01, 0x20, 0x01, 0x28, 0x09, 0x52, 0x04, 0x6e, 0x61, 0x6d, 0x65, 0x12,
	0x12, 0x0a, 0x04, 0x70, 0x6f, 0x72, 0x74, 0x18, 0x02, 0x20, 0x01, 0x28, 0x05, 0x52, 0x04, 0x70,
	0x6f, 0x72, 0x74, 0x22, 0x81, 0x02, 0x0a, 0x11, 0x47, 0x61, 0x6d, 0x65, 0x46, 0x6c, 0x65, 0x65,
	0x74, 0x52, 0x65, 0x73, 0x70, 0x6f, 0x6e, 0x73, 0x65, 0x12, 0x19, 0x0a, 0x08, 0x66, 0x6c, 0x65,
	0x65, 0x74, 0x5f, 0x69, 0x64, 0x18, 0x01, 0x20, 0x01, 0x28, 0x09, 0x52, 0x07, 0x66, 0x6c, 0x65,
	0x65, 0x74, 0x49, 0x64, 0x12, 0x1c, 0x0a, 0x09, 0x6e, 0x61, 0x6d, 0x65, 0x73, 0x70, 0x61, 0x63,
	0x65, 0x18, 0x02, 0x20, 0x01, 0x28, 0x09, 0x52, 0x09, 0x6e, 0x61, 0x6d, 0x65, 0x73, 0x70, 0x61,
	0x63, 0x65, 0x12, 0x2d, 0x0a, 0x0a, 0x66, 0x6c, 0x65, 0x65, 0x74, 0x5f, 0x74, 0x79, 0x70, 0x65,
	0x18, 0x03, 0x20, 0x01, 0x28, 0x0e, 0x32, 0x0e, 0x2e, 0x47, 0x61, 0x6d, 0x65, 0x46, 0x6c, 0x65,
	0x65, 0x74, 0x54, 0x79, 0x70, 0x65, 0x52, 0x09, 0x66, 0x6c, 0x65, 0x65, 0x74, 0x54, 0x79, 0x70,
	0x65, 0x12, 0x1f, 0x0a, 0x0b, 0x67, 0x61, 0x6d, 0x65, 0x5f, 0x73, 0x79, 0x6d, 0x62, 0x6f, 0x6c,
	0x18, 0x04, 0x20, 0x01, 0x28, 0x09, 0x52, 0x0a, 0x67, 0x61, 0x6d, 0x65, 0x53, 0x79, 0x6d, 0x62,
	0x6f, 0x6c, 0x12, 0x33, 0x0a, 0x16, 0x6e, 0x75, 0x6d, 0x62, 0x65, 0x72, 0x5f, 0x6f, 0x66, 0x5f,
	0x67, 0x61, 0x6d, 0x65, 0x5f, 0x73, 0x65, 0x72, 0x76, 0x65, 0x72, 0x73, 0x18, 0x05, 0x20, 0x01,
	0x28, 0x03, 0x52, 0x13, 0x6e, 0x75, 0x6d, 0x62, 0x65, 0x72, 0x4f, 0x66, 0x47, 0x61, 0x6d, 0x65,
	0x53, 0x65, 0x72, 0x76, 0x65, 0x72, 0x73, 0x12, 0x2e, 0x0a, 0x0c, 0x67, 0x61, 0x6d, 0x65, 0x5f,
	0x73, 0x65, 0x72, 0x76, 0x65, 0x72, 0x73, 0x18, 0x06, 0x20, 0x03, 0x28, 0x0b, 0x32, 0x0b, 0x2e,
	0x47, 0x61, 0x6d, 0x65, 0x53, 0x65, 0x72, 0x76, 0x65, 0x72, 0x52, 0x0b, 0x67, 0x61, 0x6d, 0x65,
	0x53, 0x65, 0x72, 0x76, 0x65, 0x72, 0x73, 0x2a, 0x5a, 0x0a, 0x0d, 0x47, 0x61, 0x6d, 0x65, 0x46,
	0x6c, 0x65, 0x65, 0x74, 0x54, 0x79, 0x70, 0x65, 0x12, 0x1a, 0x0a, 0x16, 0x46, 0x4c, 0x45, 0x45,
	0x54, 0x5f, 0x54, 0x59, 0x50, 0x45, 0x5f, 0x55, 0x4e, 0x53, 0x50, 0x45, 0x43, 0x49, 0x46, 0x49,
	0x45, 0x44, 0x10, 0x00, 0x12, 0x13, 0x0a, 0x0f, 0x49, 0x53, 0x4c, 0x41, 0x4e, 0x44, 0x5f, 0x49,
	0x4e, 0x53, 0x54, 0x41, 0x4e, 0x43, 0x45, 0x10, 0x01, 0x12, 0x18, 0x0a, 0x14, 0x47, 0x41, 0x4d,
	0x45, 0x5f, 0x53, 0x45, 0x52, 0x56, 0x45, 0x52, 0x5f, 0x49, 0x4e, 0x53, 0x54, 0x41, 0x4e, 0x43,
	0x45, 0x10, 0x02, 0x42, 0x83, 0x01, 0x0a, 0x24, 0x69, 0x6f, 0x2e, 0x6d, 0x75, 0x7a, 0x69, 0x76,
	0x65, 0x72, 0x73, 0x65, 0x2e, 0x70, 0x72, 0x6f, 0x74, 0x6f, 0x2e, 0x6b, 0x75, 0x62, 0x65, 0x72,
	0x6e, 0x65, 0x74, 0x65, 0x73, 0x2e, 0x64, 0x6f, 0x6d, 0x61, 0x69, 0x6e, 0x42, 0x0b, 0x43, 0x6f,
	0x6d, 0x6d, 0x6f, 0x6e, 0x50, 0x72, 0x6f, 0x74, 0x6f, 0x50, 0x01, 0x5a, 0x4c, 0x67, 0x69, 0x74,
	0x6c, 0x61, 0x62, 0x2e, 0x63, 0x6f, 0x6d, 0x2f, 0x6d, 0x75, 0x7a, 0x69, 0x76, 0x65, 0x72, 0x73,
	0x65, 0x2f, 0x63, 0x6f, 0x6d, 0x6d, 0x6f, 0x6e, 0x2f, 0x70, 0x72, 0x6f, 0x74, 0x6f, 0x67, 0x6f,
	0x67, 0x65, 0x6e, 0x65, 0x72, 0x61, 0x74, 0x6f, 0x72, 0x2f, 0x6b, 0x75, 0x62, 0x65, 0x72, 0x6e,
	0x65, 0x74, 0x65, 0x73, 0x2d, 0x70, 0x72, 0x6f, 0x74, 0x6f, 0x2d, 0x61, 0x70, 0x69, 0x2f, 0x70,
	0x6b, 0x67, 0x2f, 0x64, 0x6f, 0x6d, 0x61, 0x69, 0x6e, 0x62, 0x06, 0x70, 0x72, 0x6f, 0x74, 0x6f,
	0x33,
}

var (
	file_domain_k8s_common_proto_rawDescOnce sync.Once
	file_domain_k8s_common_proto_rawDescData = file_domain_k8s_common_proto_rawDesc
)

func file_domain_k8s_common_proto_rawDescGZIP() []byte {
	file_domain_k8s_common_proto_rawDescOnce.Do(func() {
		file_domain_k8s_common_proto_rawDescData = protoimpl.X.CompressGZIP(file_domain_k8s_common_proto_rawDescData)
	})
	return file_domain_k8s_common_proto_rawDescData
}

var file_domain_k8s_common_proto_enumTypes = make([]protoimpl.EnumInfo, 1)
var file_domain_k8s_common_proto_msgTypes = make([]protoimpl.MessageInfo, 3)
var file_domain_k8s_common_proto_goTypes = []interface{}{
	(GameFleetType)(0),        // 0: GameFleetType
	(*GameServer)(nil),        // 1: GameServer
	(*GameFleetResponse)(nil), // 2: GameFleetResponse
	(*GameServer_Port)(nil),   // 3: GameServer.Port
}
var file_domain_k8s_common_proto_depIdxs = []int32{
	3, // 0: GameServer.ports:type_name -> GameServer.Port
	0, // 1: GameFleetResponse.fleet_type:type_name -> GameFleetType
	1, // 2: GameFleetResponse.game_servers:type_name -> GameServer
	3, // [3:3] is the sub-list for method output_type
	3, // [3:3] is the sub-list for method input_type
	3, // [3:3] is the sub-list for extension type_name
	3, // [3:3] is the sub-list for extension extendee
	0, // [0:3] is the sub-list for field type_name
}

func init() { file_domain_k8s_common_proto_init() }
func file_domain_k8s_common_proto_init() {
	if File_domain_k8s_common_proto != nil {
		return
	}
	if !protoimpl.UnsafeEnabled {
		file_domain_k8s_common_proto_msgTypes[0].Exporter = func(v interface{}, i int) interface{} {
			switch v := v.(*GameServer); i {
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
		file_domain_k8s_common_proto_msgTypes[1].Exporter = func(v interface{}, i int) interface{} {
			switch v := v.(*GameFleetResponse); i {
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
		file_domain_k8s_common_proto_msgTypes[2].Exporter = func(v interface{}, i int) interface{} {
			switch v := v.(*GameServer_Port); i {
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
			RawDescriptor: file_domain_k8s_common_proto_rawDesc,
			NumEnums:      1,
			NumMessages:   3,
			NumExtensions: 0,
			NumServices:   0,
		},
		GoTypes:           file_domain_k8s_common_proto_goTypes,
		DependencyIndexes: file_domain_k8s_common_proto_depIdxs,
		EnumInfos:         file_domain_k8s_common_proto_enumTypes,
		MessageInfos:      file_domain_k8s_common_proto_msgTypes,
	}.Build()
	File_domain_k8s_common_proto = out.File
	file_domain_k8s_common_proto_rawDesc = nil
	file_domain_k8s_common_proto_goTypes = nil
	file_domain_k8s_common_proto_depIdxs = nil
}
