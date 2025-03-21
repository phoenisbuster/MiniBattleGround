// Code generated by protoc-gen-go-grpc. DO NOT EDIT.
// versions:
// - protoc-gen-go-grpc v1.2.0
// - protoc             v3.19.1
// source: game_content_quest_filter.proto

package quest

import (
	context "context"
	domain "gitlab.com/muziverse/common/protogogenerator/game-content-proto-api/domain"
	grpc "google.golang.org/grpc"
	codes "google.golang.org/grpc/codes"
	status "google.golang.org/grpc/status"
)

// This is a compile-time assertion to ensure that this generated file
// is compatible with the grpc package it is being compiled against.
// Requires gRPC-Go v1.32.0 or later.
const _ = grpc.SupportPackageIsVersion7

// GameContentQuestFilterServiceClient is the client API for GameContentQuestFilterService service.
//
// For semantics around ctx use and closing/ending streaming RPCs, please refer to https://pkg.go.dev/google.golang.org/grpc/?tab=doc#ClientConn.NewStream.
type GameContentQuestFilterServiceClient interface {
	Filter(ctx context.Context, in *QuestFilterRequest, opts ...grpc.CallOption) (*domain.ListQuestResponse, error)
}

type gameContentQuestFilterServiceClient struct {
	cc grpc.ClientConnInterface
}

func NewGameContentQuestFilterServiceClient(cc grpc.ClientConnInterface) GameContentQuestFilterServiceClient {
	return &gameContentQuestFilterServiceClient{cc}
}

func (c *gameContentQuestFilterServiceClient) Filter(ctx context.Context, in *QuestFilterRequest, opts ...grpc.CallOption) (*domain.ListQuestResponse, error) {
	out := new(domain.ListQuestResponse)
	err := c.cc.Invoke(ctx, "/gamecontent.quest.GameContentQuestFilterService/Filter", in, out, opts...)
	if err != nil {
		return nil, err
	}
	return out, nil
}

// GameContentQuestFilterServiceServer is the server API for GameContentQuestFilterService service.
// All implementations must embed UnimplementedGameContentQuestFilterServiceServer
// for forward compatibility
type GameContentQuestFilterServiceServer interface {
	Filter(context.Context, *QuestFilterRequest) (*domain.ListQuestResponse, error)
	mustEmbedUnimplementedGameContentQuestFilterServiceServer()
}

// UnimplementedGameContentQuestFilterServiceServer must be embedded to have forward compatible implementations.
type UnimplementedGameContentQuestFilterServiceServer struct {
}

func (UnimplementedGameContentQuestFilterServiceServer) Filter(context.Context, *QuestFilterRequest) (*domain.ListQuestResponse, error) {
	return nil, status.Errorf(codes.Unimplemented, "method Filter not implemented")
}
func (UnimplementedGameContentQuestFilterServiceServer) mustEmbedUnimplementedGameContentQuestFilterServiceServer() {
}

// UnsafeGameContentQuestFilterServiceServer may be embedded to opt out of forward compatibility for this service.
// Use of this interface is not recommended, as added methods to GameContentQuestFilterServiceServer will
// result in compilation errors.
type UnsafeGameContentQuestFilterServiceServer interface {
	mustEmbedUnimplementedGameContentQuestFilterServiceServer()
}

func RegisterGameContentQuestFilterServiceServer(s grpc.ServiceRegistrar, srv GameContentQuestFilterServiceServer) {
	s.RegisterService(&GameContentQuestFilterService_ServiceDesc, srv)
}

func _GameContentQuestFilterService_Filter_Handler(srv interface{}, ctx context.Context, dec func(interface{}) error, interceptor grpc.UnaryServerInterceptor) (interface{}, error) {
	in := new(QuestFilterRequest)
	if err := dec(in); err != nil {
		return nil, err
	}
	if interceptor == nil {
		return srv.(GameContentQuestFilterServiceServer).Filter(ctx, in)
	}
	info := &grpc.UnaryServerInfo{
		Server:     srv,
		FullMethod: "/gamecontent.quest.GameContentQuestFilterService/Filter",
	}
	handler := func(ctx context.Context, req interface{}) (interface{}, error) {
		return srv.(GameContentQuestFilterServiceServer).Filter(ctx, req.(*QuestFilterRequest))
	}
	return interceptor(ctx, in, info, handler)
}

// GameContentQuestFilterService_ServiceDesc is the grpc.ServiceDesc for GameContentQuestFilterService service.
// It's only intended for direct use with grpc.RegisterService,
// and not to be introspected or modified (even as a copy)
var GameContentQuestFilterService_ServiceDesc = grpc.ServiceDesc{
	ServiceName: "gamecontent.quest.GameContentQuestFilterService",
	HandlerType: (*GameContentQuestFilterServiceServer)(nil),
	Methods: []grpc.MethodDesc{
		{
			MethodName: "Filter",
			Handler:    _GameContentQuestFilterService_Filter_Handler,
		},
	},
	Streams:  []grpc.StreamDesc{},
	Metadata: "game_content_quest_filter.proto",
}
