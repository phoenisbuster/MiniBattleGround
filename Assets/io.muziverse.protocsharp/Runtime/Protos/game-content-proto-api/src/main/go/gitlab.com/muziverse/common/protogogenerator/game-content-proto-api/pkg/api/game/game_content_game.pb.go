// Code generated by protoc-gen-go. DO NOT EDIT.
// versions:
// 	protoc-gen-go v1.27.1
// 	protoc        v3.19.1
// source: game_content_game.proto

package game

import (
	_ "github.com/envoyproxy/protoc-gen-validate/validate"
	domain "gitlab.com/muziverse/common/protogogenerator/game-content-proto-api/pkg/domain"
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

type GetGameRequest struct {
	state         protoimpl.MessageState
	sizeCache     protoimpl.SizeCache
	unknownFields protoimpl.UnknownFields

	GameSymbol         string `protobuf:"bytes,1,opt,name=game_symbol,json=gameSymbol,proto3" json:"game_symbol,omitempty"`
	IslandInstanceCode string `protobuf:"bytes,2,opt,name=island_instance_code,json=islandInstanceCode,proto3" json:"island_instance_code,omitempty"`
}

func (x *GetGameRequest) Reset() {
	*x = GetGameRequest{}
	if protoimpl.UnsafeEnabled {
		mi := &file_game_content_game_proto_msgTypes[0]
		ms := protoimpl.X.MessageStateOf(protoimpl.Pointer(x))
		ms.StoreMessageInfo(mi)
	}
}

func (x *GetGameRequest) String() string {
	return protoimpl.X.MessageStringOf(x)
}

func (*GetGameRequest) ProtoMessage() {}

func (x *GetGameRequest) ProtoReflect() protoreflect.Message {
	mi := &file_game_content_game_proto_msgTypes[0]
	if protoimpl.UnsafeEnabled && x != nil {
		ms := protoimpl.X.MessageStateOf(protoimpl.Pointer(x))
		if ms.LoadMessageInfo() == nil {
			ms.StoreMessageInfo(mi)
		}
		return ms
	}
	return mi.MessageOf(x)
}

// Deprecated: Use GetGameRequest.ProtoReflect.Descriptor instead.
func (*GetGameRequest) Descriptor() ([]byte, []int) {
	return file_game_content_game_proto_rawDescGZIP(), []int{0}
}

func (x *GetGameRequest) GetGameSymbol() string {
	if x != nil {
		return x.GameSymbol
	}
	return ""
}

func (x *GetGameRequest) GetIslandInstanceCode() string {
	if x != nil {
		return x.IslandInstanceCode
	}
	return ""
}

type CreateGameRequest struct {
	state         protoimpl.MessageState
	sizeCache     protoimpl.SizeCache
	unknownFields protoimpl.UnknownFields

	GameName    string                   `protobuf:"bytes,1,opt,name=game_name,json=gameName,proto3" json:"game_name,omitempty"`
	GameSymbol  string                   `protobuf:"bytes,2,opt,name=game_symbol,json=gameSymbol,proto3" json:"game_symbol,omitempty"`
	PlayingMode []domain.GamePlayingMode `protobuf:"varint,3,rep,packed,name=playing_mode,json=playingMode,proto3,enum=GamePlayingMode" json:"playing_mode,omitempty"`
}

func (x *CreateGameRequest) Reset() {
	*x = CreateGameRequest{}
	if protoimpl.UnsafeEnabled {
		mi := &file_game_content_game_proto_msgTypes[1]
		ms := protoimpl.X.MessageStateOf(protoimpl.Pointer(x))
		ms.StoreMessageInfo(mi)
	}
}

func (x *CreateGameRequest) String() string {
	return protoimpl.X.MessageStringOf(x)
}

func (*CreateGameRequest) ProtoMessage() {}

func (x *CreateGameRequest) ProtoReflect() protoreflect.Message {
	mi := &file_game_content_game_proto_msgTypes[1]
	if protoimpl.UnsafeEnabled && x != nil {
		ms := protoimpl.X.MessageStateOf(protoimpl.Pointer(x))
		if ms.LoadMessageInfo() == nil {
			ms.StoreMessageInfo(mi)
		}
		return ms
	}
	return mi.MessageOf(x)
}

// Deprecated: Use CreateGameRequest.ProtoReflect.Descriptor instead.
func (*CreateGameRequest) Descriptor() ([]byte, []int) {
	return file_game_content_game_proto_rawDescGZIP(), []int{1}
}

func (x *CreateGameRequest) GetGameName() string {
	if x != nil {
		return x.GameName
	}
	return ""
}

func (x *CreateGameRequest) GetGameSymbol() string {
	if x != nil {
		return x.GameSymbol
	}
	return ""
}

func (x *CreateGameRequest) GetPlayingMode() []domain.GamePlayingMode {
	if x != nil {
		return x.PlayingMode
	}
	return nil
}

type GetListGameResponse struct {
	state         protoimpl.MessageState
	sizeCache     protoimpl.SizeCache
	unknownFields protoimpl.UnknownFields

	Data []*domain.GameResponse `protobuf:"bytes,1,rep,name=data,proto3" json:"data,omitempty"`
}

func (x *GetListGameResponse) Reset() {
	*x = GetListGameResponse{}
	if protoimpl.UnsafeEnabled {
		mi := &file_game_content_game_proto_msgTypes[2]
		ms := protoimpl.X.MessageStateOf(protoimpl.Pointer(x))
		ms.StoreMessageInfo(mi)
	}
}

func (x *GetListGameResponse) String() string {
	return protoimpl.X.MessageStringOf(x)
}

func (*GetListGameResponse) ProtoMessage() {}

func (x *GetListGameResponse) ProtoReflect() protoreflect.Message {
	mi := &file_game_content_game_proto_msgTypes[2]
	if protoimpl.UnsafeEnabled && x != nil {
		ms := protoimpl.X.MessageStateOf(protoimpl.Pointer(x))
		if ms.LoadMessageInfo() == nil {
			ms.StoreMessageInfo(mi)
		}
		return ms
	}
	return mi.MessageOf(x)
}

// Deprecated: Use GetListGameResponse.ProtoReflect.Descriptor instead.
func (*GetListGameResponse) Descriptor() ([]byte, []int) {
	return file_game_content_game_proto_rawDescGZIP(), []int{2}
}

func (x *GetListGameResponse) GetData() []*domain.GameResponse {
	if x != nil {
		return x.Data
	}
	return nil
}

type UpdateGameRequest struct {
	state         protoimpl.MessageState
	sizeCache     protoimpl.SizeCache
	unknownFields protoimpl.UnknownFields

	Id          int64                    `protobuf:"varint,1,opt,name=id,proto3" json:"id,omitempty"`
	GameName    string                   `protobuf:"bytes,2,opt,name=game_name,json=gameName,proto3" json:"game_name,omitempty"`
	GameSymbol  string                   `protobuf:"bytes,3,opt,name=game_symbol,json=gameSymbol,proto3" json:"game_symbol,omitempty"`
	PlayingMode []domain.GamePlayingMode `protobuf:"varint,4,rep,packed,name=playing_mode,json=playingMode,proto3,enum=GamePlayingMode" json:"playing_mode,omitempty"`
}

func (x *UpdateGameRequest) Reset() {
	*x = UpdateGameRequest{}
	if protoimpl.UnsafeEnabled {
		mi := &file_game_content_game_proto_msgTypes[3]
		ms := protoimpl.X.MessageStateOf(protoimpl.Pointer(x))
		ms.StoreMessageInfo(mi)
	}
}

func (x *UpdateGameRequest) String() string {
	return protoimpl.X.MessageStringOf(x)
}

func (*UpdateGameRequest) ProtoMessage() {}

func (x *UpdateGameRequest) ProtoReflect() protoreflect.Message {
	mi := &file_game_content_game_proto_msgTypes[3]
	if protoimpl.UnsafeEnabled && x != nil {
		ms := protoimpl.X.MessageStateOf(protoimpl.Pointer(x))
		if ms.LoadMessageInfo() == nil {
			ms.StoreMessageInfo(mi)
		}
		return ms
	}
	return mi.MessageOf(x)
}

// Deprecated: Use UpdateGameRequest.ProtoReflect.Descriptor instead.
func (*UpdateGameRequest) Descriptor() ([]byte, []int) {
	return file_game_content_game_proto_rawDescGZIP(), []int{3}
}

func (x *UpdateGameRequest) GetId() int64 {
	if x != nil {
		return x.Id
	}
	return 0
}

func (x *UpdateGameRequest) GetGameName() string {
	if x != nil {
		return x.GameName
	}
	return ""
}

func (x *UpdateGameRequest) GetGameSymbol() string {
	if x != nil {
		return x.GameSymbol
	}
	return ""
}

func (x *UpdateGameRequest) GetPlayingMode() []domain.GamePlayingMode {
	if x != nil {
		return x.PlayingMode
	}
	return nil
}

var File_game_content_game_proto protoreflect.FileDescriptor

var file_game_content_game_proto_rawDesc = []byte{
	0x0a, 0x17, 0x67, 0x61, 0x6d, 0x65, 0x5f, 0x63, 0x6f, 0x6e, 0x74, 0x65, 0x6e, 0x74, 0x5f, 0x67,
	0x61, 0x6d, 0x65, 0x2e, 0x70, 0x72, 0x6f, 0x74, 0x6f, 0x12, 0x10, 0x67, 0x61, 0x6d, 0x65, 0x63,
	0x6f, 0x6e, 0x74, 0x65, 0x6e, 0x74, 0x2e, 0x67, 0x61, 0x6d, 0x65, 0x1a, 0x20, 0x64, 0x6f, 0x6d,
	0x61, 0x69, 0x6e, 0x2f, 0x67, 0x61, 0x6d, 0x65, 0x5f, 0x63, 0x6f, 0x6e, 0x74, 0x65, 0x6e, 0x74,
	0x5f, 0x63, 0x6f, 0x6d, 0x6d, 0x6f, 0x6e, 0x2e, 0x70, 0x72, 0x6f, 0x74, 0x6f, 0x1a, 0x26, 0x64,
	0x6f, 0x6d, 0x61, 0x69, 0x6e, 0x2f, 0x67, 0x61, 0x6d, 0x65, 0x5f, 0x63, 0x6f, 0x6e, 0x74, 0x65,
	0x6e, 0x74, 0x5f, 0x70, 0x6c, 0x61, 0x79, 0x69, 0x6e, 0x67, 0x5f, 0x6d, 0x6f, 0x64, 0x65, 0x2e,
	0x70, 0x72, 0x6f, 0x74, 0x6f, 0x1a, 0x1b, 0x67, 0x6f, 0x6f, 0x67, 0x6c, 0x65, 0x2f, 0x70, 0x72,
	0x6f, 0x74, 0x6f, 0x62, 0x75, 0x66, 0x2f, 0x65, 0x6d, 0x70, 0x74, 0x79, 0x2e, 0x70, 0x72, 0x6f,
	0x74, 0x6f, 0x1a, 0x17, 0x76, 0x61, 0x6c, 0x69, 0x64, 0x61, 0x74, 0x65, 0x2f, 0x76, 0x61, 0x6c,
	0x69, 0x64, 0x61, 0x74, 0x65, 0x2e, 0x70, 0x72, 0x6f, 0x74, 0x6f, 0x22, 0x6c, 0x0a, 0x0e, 0x47,
	0x65, 0x74, 0x47, 0x61, 0x6d, 0x65, 0x52, 0x65, 0x71, 0x75, 0x65, 0x73, 0x74, 0x12, 0x28, 0x0a,
	0x0b, 0x67, 0x61, 0x6d, 0x65, 0x5f, 0x73, 0x79, 0x6d, 0x62, 0x6f, 0x6c, 0x18, 0x01, 0x20, 0x01,
	0x28, 0x09, 0x42, 0x07, 0xfa, 0x42, 0x04, 0x72, 0x02, 0x10, 0x01, 0x52, 0x0a, 0x67, 0x61, 0x6d,
	0x65, 0x53, 0x79, 0x6d, 0x62, 0x6f, 0x6c, 0x12, 0x30, 0x0a, 0x14, 0x69, 0x73, 0x6c, 0x61, 0x6e,
	0x64, 0x5f, 0x69, 0x6e, 0x73, 0x74, 0x61, 0x6e, 0x63, 0x65, 0x5f, 0x63, 0x6f, 0x64, 0x65, 0x18,
	0x02, 0x20, 0x01, 0x28, 0x09, 0x52, 0x12, 0x69, 0x73, 0x6c, 0x61, 0x6e, 0x64, 0x49, 0x6e, 0x73,
	0x74, 0x61, 0x6e, 0x63, 0x65, 0x43, 0x6f, 0x64, 0x65, 0x22, 0x86, 0x01, 0x0a, 0x11, 0x43, 0x72,
	0x65, 0x61, 0x74, 0x65, 0x47, 0x61, 0x6d, 0x65, 0x52, 0x65, 0x71, 0x75, 0x65, 0x73, 0x74, 0x12,
	0x1b, 0x0a, 0x09, 0x67, 0x61, 0x6d, 0x65, 0x5f, 0x6e, 0x61, 0x6d, 0x65, 0x18, 0x01, 0x20, 0x01,
	0x28, 0x09, 0x52, 0x08, 0x67, 0x61, 0x6d, 0x65, 0x4e, 0x61, 0x6d, 0x65, 0x12, 0x1f, 0x0a, 0x0b,
	0x67, 0x61, 0x6d, 0x65, 0x5f, 0x73, 0x79, 0x6d, 0x62, 0x6f, 0x6c, 0x18, 0x02, 0x20, 0x01, 0x28,
	0x09, 0x52, 0x0a, 0x67, 0x61, 0x6d, 0x65, 0x53, 0x79, 0x6d, 0x62, 0x6f, 0x6c, 0x12, 0x33, 0x0a,
	0x0c, 0x70, 0x6c, 0x61, 0x79, 0x69, 0x6e, 0x67, 0x5f, 0x6d, 0x6f, 0x64, 0x65, 0x18, 0x03, 0x20,
	0x03, 0x28, 0x0e, 0x32, 0x10, 0x2e, 0x47, 0x61, 0x6d, 0x65, 0x50, 0x6c, 0x61, 0x79, 0x69, 0x6e,
	0x67, 0x4d, 0x6f, 0x64, 0x65, 0x52, 0x0b, 0x70, 0x6c, 0x61, 0x79, 0x69, 0x6e, 0x67, 0x4d, 0x6f,
	0x64, 0x65, 0x22, 0x38, 0x0a, 0x13, 0x47, 0x65, 0x74, 0x4c, 0x69, 0x73, 0x74, 0x47, 0x61, 0x6d,
	0x65, 0x52, 0x65, 0x73, 0x70, 0x6f, 0x6e, 0x73, 0x65, 0x12, 0x21, 0x0a, 0x04, 0x64, 0x61, 0x74,
	0x61, 0x18, 0x01, 0x20, 0x03, 0x28, 0x0b, 0x32, 0x0d, 0x2e, 0x47, 0x61, 0x6d, 0x65, 0x52, 0x65,
	0x73, 0x70, 0x6f, 0x6e, 0x73, 0x65, 0x52, 0x04, 0x64, 0x61, 0x74, 0x61, 0x22, 0x96, 0x01, 0x0a,
	0x11, 0x55, 0x70, 0x64, 0x61, 0x74, 0x65, 0x47, 0x61, 0x6d, 0x65, 0x52, 0x65, 0x71, 0x75, 0x65,
	0x73, 0x74, 0x12, 0x0e, 0x0a, 0x02, 0x69, 0x64, 0x18, 0x01, 0x20, 0x01, 0x28, 0x03, 0x52, 0x02,
	0x69, 0x64, 0x12, 0x1b, 0x0a, 0x09, 0x67, 0x61, 0x6d, 0x65, 0x5f, 0x6e, 0x61, 0x6d, 0x65, 0x18,
	0x02, 0x20, 0x01, 0x28, 0x09, 0x52, 0x08, 0x67, 0x61, 0x6d, 0x65, 0x4e, 0x61, 0x6d, 0x65, 0x12,
	0x1f, 0x0a, 0x0b, 0x67, 0x61, 0x6d, 0x65, 0x5f, 0x73, 0x79, 0x6d, 0x62, 0x6f, 0x6c, 0x18, 0x03,
	0x20, 0x01, 0x28, 0x09, 0x52, 0x0a, 0x67, 0x61, 0x6d, 0x65, 0x53, 0x79, 0x6d, 0x62, 0x6f, 0x6c,
	0x12, 0x33, 0x0a, 0x0c, 0x70, 0x6c, 0x61, 0x79, 0x69, 0x6e, 0x67, 0x5f, 0x6d, 0x6f, 0x64, 0x65,
	0x18, 0x04, 0x20, 0x03, 0x28, 0x0e, 0x32, 0x10, 0x2e, 0x47, 0x61, 0x6d, 0x65, 0x50, 0x6c, 0x61,
	0x79, 0x69, 0x6e, 0x67, 0x4d, 0x6f, 0x64, 0x65, 0x52, 0x0b, 0x70, 0x6c, 0x61, 0x79, 0x69, 0x6e,
	0x67, 0x4d, 0x6f, 0x64, 0x65, 0x32, 0xbe, 0x02, 0x0a, 0x0b, 0x47, 0x61, 0x6d, 0x65, 0x53, 0x65,
	0x72, 0x76, 0x69, 0x63, 0x65, 0x12, 0x44, 0x0a, 0x0f, 0x47, 0x65, 0x74, 0x47, 0x61, 0x6d, 0x65,
	0x42, 0x79, 0x53, 0x79, 0x6d, 0x62, 0x6f, 0x6c, 0x12, 0x20, 0x2e, 0x67, 0x61, 0x6d, 0x65, 0x63,
	0x6f, 0x6e, 0x74, 0x65, 0x6e, 0x74, 0x2e, 0x67, 0x61, 0x6d, 0x65, 0x2e, 0x47, 0x65, 0x74, 0x47,
	0x61, 0x6d, 0x65, 0x52, 0x65, 0x71, 0x75, 0x65, 0x73, 0x74, 0x1a, 0x0d, 0x2e, 0x47, 0x61, 0x6d,
	0x65, 0x52, 0x65, 0x73, 0x70, 0x6f, 0x6e, 0x73, 0x65, 0x22, 0x00, 0x12, 0x4f, 0x0a, 0x0c, 0x47,
	0x65, 0x74, 0x4c, 0x69, 0x73, 0x74, 0x47, 0x61, 0x6d, 0x65, 0x73, 0x12, 0x16, 0x2e, 0x67, 0x6f,
	0x6f, 0x67, 0x6c, 0x65, 0x2e, 0x70, 0x72, 0x6f, 0x74, 0x6f, 0x62, 0x75, 0x66, 0x2e, 0x45, 0x6d,
	0x70, 0x74, 0x79, 0x1a, 0x25, 0x2e, 0x67, 0x61, 0x6d, 0x65, 0x63, 0x6f, 0x6e, 0x74, 0x65, 0x6e,
	0x74, 0x2e, 0x67, 0x61, 0x6d, 0x65, 0x2e, 0x47, 0x65, 0x74, 0x4c, 0x69, 0x73, 0x74, 0x47, 0x61,
	0x6d, 0x65, 0x52, 0x65, 0x73, 0x70, 0x6f, 0x6e, 0x73, 0x65, 0x22, 0x00, 0x12, 0x4b, 0x0a, 0x0a,
	0x43, 0x72, 0x65, 0x61, 0x74, 0x65, 0x47, 0x61, 0x6d, 0x65, 0x12, 0x23, 0x2e, 0x67, 0x61, 0x6d,
	0x65, 0x63, 0x6f, 0x6e, 0x74, 0x65, 0x6e, 0x74, 0x2e, 0x67, 0x61, 0x6d, 0x65, 0x2e, 0x43, 0x72,
	0x65, 0x61, 0x74, 0x65, 0x47, 0x61, 0x6d, 0x65, 0x52, 0x65, 0x71, 0x75, 0x65, 0x73, 0x74, 0x1a,
	0x16, 0x2e, 0x67, 0x6f, 0x6f, 0x67, 0x6c, 0x65, 0x2e, 0x70, 0x72, 0x6f, 0x74, 0x6f, 0x62, 0x75,
	0x66, 0x2e, 0x45, 0x6d, 0x70, 0x74, 0x79, 0x22, 0x00, 0x12, 0x4b, 0x0a, 0x0a, 0x55, 0x70, 0x64,
	0x61, 0x74, 0x65, 0x47, 0x61, 0x6d, 0x65, 0x12, 0x23, 0x2e, 0x67, 0x61, 0x6d, 0x65, 0x63, 0x6f,
	0x6e, 0x74, 0x65, 0x6e, 0x74, 0x2e, 0x67, 0x61, 0x6d, 0x65, 0x2e, 0x55, 0x70, 0x64, 0x61, 0x74,
	0x65, 0x47, 0x61, 0x6d, 0x65, 0x52, 0x65, 0x71, 0x75, 0x65, 0x73, 0x74, 0x1a, 0x16, 0x2e, 0x67,
	0x6f, 0x6f, 0x67, 0x6c, 0x65, 0x2e, 0x70, 0x72, 0x6f, 0x74, 0x6f, 0x62, 0x75, 0x66, 0x2e, 0x45,
	0x6d, 0x70, 0x74, 0x79, 0x22, 0x00, 0x42, 0xba, 0x01, 0x0a, 0x27, 0x69, 0x6f, 0x2e, 0x6d, 0x75,
	0x7a, 0x69, 0x76, 0x65, 0x72, 0x73, 0x65, 0x2e, 0x70, 0x72, 0x6f, 0x74, 0x6f, 0x2e, 0x67, 0x61,
	0x6d, 0x65, 0x63, 0x6f, 0x6e, 0x74, 0x65, 0x6e, 0x74, 0x2e, 0x61, 0x70, 0x69, 0x2e, 0x67, 0x61,
	0x6d, 0x65, 0x42, 0x14, 0x47, 0x61, 0x6d, 0x65, 0x43, 0x6f, 0x6e, 0x74, 0x65, 0x6e, 0x74, 0x47,
	0x61, 0x6d, 0x65, 0x50, 0x72, 0x6f, 0x74, 0x6f, 0x50, 0x01, 0x5a, 0x50, 0x67, 0x69, 0x74, 0x6c,
	0x61, 0x62, 0x2e, 0x63, 0x6f, 0x6d, 0x2f, 0x6d, 0x75, 0x7a, 0x69, 0x76, 0x65, 0x72, 0x73, 0x65,
	0x2f, 0x63, 0x6f, 0x6d, 0x6d, 0x6f, 0x6e, 0x2f, 0x70, 0x72, 0x6f, 0x74, 0x6f, 0x67, 0x6f, 0x67,
	0x65, 0x6e, 0x65, 0x72, 0x61, 0x74, 0x6f, 0x72, 0x2f, 0x67, 0x61, 0x6d, 0x65, 0x2d, 0x63, 0x6f,
	0x6e, 0x74, 0x65, 0x6e, 0x74, 0x2d, 0x70, 0x72, 0x6f, 0x74, 0x6f, 0x2d, 0x61, 0x70, 0x69, 0x2f,
	0x70, 0x6b, 0x67, 0x2f, 0x61, 0x70, 0x69, 0x2f, 0x67, 0x61, 0x6d, 0x65, 0xaa, 0x02, 0x24, 0x4d,
	0x75, 0x7a, 0x69, 0x76, 0x65, 0x72, 0x73, 0x65, 0x2e, 0x50, 0x72, 0x6f, 0x74, 0x6f, 0x2e, 0x47,
	0x61, 0x6d, 0x65, 0x43, 0x6f, 0x6e, 0x74, 0x65, 0x6e, 0x74, 0x2e, 0x41, 0x70, 0x69, 0x2e, 0x47,
	0x61, 0x6d, 0x65, 0x62, 0x06, 0x70, 0x72, 0x6f, 0x74, 0x6f, 0x33,
}

var (
	file_game_content_game_proto_rawDescOnce sync.Once
	file_game_content_game_proto_rawDescData = file_game_content_game_proto_rawDesc
)

func file_game_content_game_proto_rawDescGZIP() []byte {
	file_game_content_game_proto_rawDescOnce.Do(func() {
		file_game_content_game_proto_rawDescData = protoimpl.X.CompressGZIP(file_game_content_game_proto_rawDescData)
	})
	return file_game_content_game_proto_rawDescData
}

var file_game_content_game_proto_msgTypes = make([]protoimpl.MessageInfo, 4)
var file_game_content_game_proto_goTypes = []interface{}{
	(*GetGameRequest)(nil),      // 0: gamecontent.game.GetGameRequest
	(*CreateGameRequest)(nil),   // 1: gamecontent.game.CreateGameRequest
	(*GetListGameResponse)(nil), // 2: gamecontent.game.GetListGameResponse
	(*UpdateGameRequest)(nil),   // 3: gamecontent.game.UpdateGameRequest
	(domain.GamePlayingMode)(0), // 4: GamePlayingMode
	(*domain.GameResponse)(nil), // 5: GameResponse
	(*emptypb.Empty)(nil),       // 6: google.protobuf.Empty
}
var file_game_content_game_proto_depIdxs = []int32{
	4, // 0: gamecontent.game.CreateGameRequest.playing_mode:type_name -> GamePlayingMode
	5, // 1: gamecontent.game.GetListGameResponse.data:type_name -> GameResponse
	4, // 2: gamecontent.game.UpdateGameRequest.playing_mode:type_name -> GamePlayingMode
	0, // 3: gamecontent.game.GameService.GetGameBySymbol:input_type -> gamecontent.game.GetGameRequest
	6, // 4: gamecontent.game.GameService.GetListGames:input_type -> google.protobuf.Empty
	1, // 5: gamecontent.game.GameService.CreateGame:input_type -> gamecontent.game.CreateGameRequest
	3, // 6: gamecontent.game.GameService.UpdateGame:input_type -> gamecontent.game.UpdateGameRequest
	5, // 7: gamecontent.game.GameService.GetGameBySymbol:output_type -> GameResponse
	2, // 8: gamecontent.game.GameService.GetListGames:output_type -> gamecontent.game.GetListGameResponse
	6, // 9: gamecontent.game.GameService.CreateGame:output_type -> google.protobuf.Empty
	6, // 10: gamecontent.game.GameService.UpdateGame:output_type -> google.protobuf.Empty
	7, // [7:11] is the sub-list for method output_type
	3, // [3:7] is the sub-list for method input_type
	3, // [3:3] is the sub-list for extension type_name
	3, // [3:3] is the sub-list for extension extendee
	0, // [0:3] is the sub-list for field type_name
}

func init() { file_game_content_game_proto_init() }
func file_game_content_game_proto_init() {
	if File_game_content_game_proto != nil {
		return
	}
	if !protoimpl.UnsafeEnabled {
		file_game_content_game_proto_msgTypes[0].Exporter = func(v interface{}, i int) interface{} {
			switch v := v.(*GetGameRequest); i {
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
		file_game_content_game_proto_msgTypes[1].Exporter = func(v interface{}, i int) interface{} {
			switch v := v.(*CreateGameRequest); i {
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
		file_game_content_game_proto_msgTypes[2].Exporter = func(v interface{}, i int) interface{} {
			switch v := v.(*GetListGameResponse); i {
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
		file_game_content_game_proto_msgTypes[3].Exporter = func(v interface{}, i int) interface{} {
			switch v := v.(*UpdateGameRequest); i {
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
			RawDescriptor: file_game_content_game_proto_rawDesc,
			NumEnums:      0,
			NumMessages:   4,
			NumExtensions: 0,
			NumServices:   1,
		},
		GoTypes:           file_game_content_game_proto_goTypes,
		DependencyIndexes: file_game_content_game_proto_depIdxs,
		MessageInfos:      file_game_content_game_proto_msgTypes,
	}.Build()
	File_game_content_game_proto = out.File
	file_game_content_game_proto_rawDesc = nil
	file_game_content_game_proto_goTypes = nil
	file_game_content_game_proto_depIdxs = nil
}
